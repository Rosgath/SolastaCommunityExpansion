﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SolastaCommunityExpansion.Api;
using SolastaCommunityExpansion.Api.Extensions;
using SolastaCommunityExpansion.Builders;
using SolastaCommunityExpansion.CustomUI;
using SolastaCommunityExpansion.Feats;
using static ActionDefinitions;
using static ActionDefinitions.ActionStatus;

namespace SolastaCommunityExpansion.Models;

public static class CustomReactionsContext
{
    private static IDamagedReactionSpell _alwaysReact;

    public static bool ForcePreferredCantrip { get; private set; } //used by actual feature
    public static bool ForcePreferredCantripUI { get; set; } //used for local UI state

    [NotNull]
    public static IDamagedReactionSpell AlwaysReactToDamaged => _alwaysReact ??= new AlwaysReactToDamagedImpl();

    public static void Load()
    {
        MakeReactDefinition(ReactionRequestWarcaster.Name);
        MakeReactDefinition(ReactionRequestSpendBundlePower.Name);
        MakeReactDefinition(ReactionRequestReactionAttack.Name(EwFeats.SentinelFeat));
    }

    private static void MakeReactDefinition(string name)
    {
        ReactionDefinitionBuilder
            .Create(DatabaseHelper.ReactionDefinitions.OpportunityAttack, name,
                DefinitionBuilder.CENamespaceGuid)
            .SetGuiPresentation(Category.Reaction)
            .AddToDB();
    }

    public static IEnumerator TryReactingToDamageWithSpell(
        [NotNull] GameLocationCharacter attacker,
        GameLocationCharacter defender,
        ActionModifier attackModifier,
        RulesetAttackMode attackMode,
        bool rangedAttack,
        RuleDefinitions.AdvantageType advantageType,
        List<EffectForm> actualEffectForms,
        RulesetEffect rulesetEffect,
        bool criticalHit,
        bool firstTarget)
    {
        var ruleCaster = attacker.RulesetCharacter;

        // Wildshape heroes shouldn't be able to cast react spells
        if (ruleCaster.IsSubstitute)
        {
            yield break;
        }

        var ruleDefender = defender.RulesetCharacter;

        if (ruleDefender == null)
        {
            yield break;
        }

        if (defender.GetActionTypeStatus(ActionType.Reaction) != Available
            || defender.GetActionStatus(Id.CastReaction, ActionScope.Battle, Available) != Available)
        {
            yield break;
        }

        ruleDefender.EnumerateUsableSpells();

        var spells = ruleDefender.UsableSpells
            .Select(s => (s, s.GetAllSubFeaturesOfType<IDamagedReactionSpell>()))
            .Where(e => e.Item2 != null && !e.Item2.Empty())
            .ToList();

        foreach (var (spell, reactions) in spells)
        {
            if (reactions.Any(r => r.CanReact(attacker, defender, attackModifier, attackMode, rangedAttack,
                    advantageType, actualEffectForms, rulesetEffect, criticalHit, firstTarget)))
            {
                yield return ReactWithSpell(spell, defender, attacker);
            }
        }
    }

    private static IEnumerator ReactWithSpell(SpellDefinition spell, GameLocationCharacter caster,
        GameLocationCharacter target)
    {
        var actionManager = ServiceRepository.GetService<IGameLocationActionService>() as GameLocationActionManager;

        if (actionManager == null)
        {
            yield break;
        }

        var ruleCaster = caster.RulesetCharacter;
        var spellSlot = ruleCaster.GetLowestSlotLevelAndRepertoireToCastSpell(spell, out var spellBook);

        if (spellBook == null)
        {
            yield break;
        }

        var ruleset = ServiceRepository.GetService<IRulesetImplementationService>();
        var reactionParams = new CharacterActionParams(caster, Id.CastReaction)
        {
            IntParameter = 0,
            RulesetEffect =
                ruleset.InstantiateEffectSpell(ruleCaster, spellBook, spell, spellSlot, false),
            IsReactionEffect = true
        };

        reactionParams.TargetCharacters.Add(target);
        reactionParams.ActionModifiers.Add(new ActionModifier());

        var reactions = actionManager.PendingReactionRequestGroups.Count;
        var reaction = new ReactionRequestCastDamageSpell(reactionParams, target, spellSlot == 0);

        actionManager.AddInterruptRequest(reaction);

        yield return WaitForReactions(actionManager, reactions);
    }

    private static IEnumerator WaitForReactions([CanBeNull] IGameLocationActionService actionService,
        int previousReactionCount)
    {
        while (actionService?.PendingReactionRequestGroups != null &&
               previousReactionCount < actionService.PendingReactionRequestGroups.Count)
        {
            yield return null;
        }
    }

    internal static void SaveReadyActionPreferredCantripPatch([CanBeNull] CharacterActionParams actionParams,
        ReadyActionType readyActionType)
    {
        if (actionParams != null && readyActionType == ReadyActionType.Cantrip)
        {
            actionParams.BoolParameter4 = ForcePreferredCantripUI;
        }
    }

    internal static void ReadReadyActionPreferredCantripPatch(CharacterActionParams actionParams)
    {
        if (actionParams is {ReadyActionType: ReadyActionType.Cantrip})
        {
            ForcePreferredCantrip = actionParams.BoolParameter4;
        }
    }

    public interface IDamagedReactionSpell
    {
        bool CanReact(GameLocationCharacter attacker, GameLocationCharacter defender, ActionModifier attackModifier,
            RulesetAttackMode attackMode, bool rangedAttack, RuleDefinitions.AdvantageType advantageType,
            List<EffectForm> actualEffectForms, RulesetEffect rulesetEffect, bool criticalHit, bool firstTarget);
    }

    private sealed class AlwaysReactToDamagedImpl : IDamagedReactionSpell
    {
        public bool CanReact(GameLocationCharacter attacker, GameLocationCharacter defender,
            ActionModifier attackModifier,
            RulesetAttackMode attackMode, bool rangedAttack, RuleDefinitions.AdvantageType advantageType,
            List<EffectForm> actualEffectForms,
            RulesetEffect rulesetEffect, bool criticalHit, bool firstTarget)
        {
            return true;
        }
    }
}
