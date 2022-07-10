﻿using System.Collections.Generic;
using System.Linq;
using SolastaCommunityExpansion.Api.Extensions;

namespace SolastaCommunityExpansion.CustomUI;

public class ReactionRequestWarcaster : ReactionRequest
{
    public const string Name = "WarcasterReaction";
    private readonly GuiCharacter guiTarget;

    private readonly string type;

    public ReactionRequestWarcaster(CharacterActionParams reactionParams)
        : base(Name, reactionParams)
    {
        BuildSuboptions();
        type = string.IsNullOrEmpty(ReactionParams.StringParameter2)
            ? Name
            : ReactionParams.StringParameter2;
        // ReactionParams.StringParameter2 = type;
        guiTarget = new GuiCharacter(reactionParams.targetCharacters[0]);
    }

    public override int SelectedSubOption
    {
        get
        {
            var spell = (ReactionParams.RulesetEffect as RulesetEffectSpell)?.SpellDefinition;
            if (spell == null)
            {
                return 0;
            }

            return ReactionParams.SpellRepertoire.KnownSpells.FindIndex(s => s == spell) + 1;
        }
    }


    public override string SuboptionTag => type;

    public override bool IsStillValid
    {
        get
        {
            var targetCharacter = ReactionParams.TargetCharacters[0];
            return ServiceRepository.GetService<IGameLocationCharacterService>().ValidCharacters
                .Contains(targetCharacter) && !targetCharacter.RulesetCharacter.IsDeadOrDyingOrUnconscious;
        }
    }

    private void BuildSuboptions()
    {
        SubOptionsAvailability.Clear();
        SubOptionsAvailability.Add(0, !reactionParams.BoolParameter4);

        var battleManager = ServiceRepository.GetService<IGameLocationBattleService>() as GameLocationBattleManager;
        var actingCharacter = reactionParams.ActingCharacter;
        var cantrips = GetValidCantrips(battleManager, actingCharacter, reactionParams.targetCharacters[0]);
        if (cantrips != null && !cantrips.Empty())
        {
            reactionParams.SpellRepertoire = new RulesetSpellRepertoire();

            var i = 1;
            foreach (var c in cantrips)
            {
                reactionParams.SpellRepertoire.KnownSpells.Add(c);
                SubOptionsAvailability.Add(i, true);
                i++;
            }
        }

        foreach (var pair in SubOptionsAvailability)
        {
            if (pair.Value)
            {
                SelectSubOption(pair.Key);
                break;
            }
        }
    }

    public static List<SpellDefinition> GetValidCantrips(GameLocationBattleManager battle,
        GameLocationCharacter character, GameLocationCharacter target)
    {
        if (battle == null)
        {
            return null;
        }

        var rulesetCharacter = character.RulesetCharacter;

        // should not trigger if a wildshape form
        if (rulesetCharacter is not RulesetCharacterHero)
        {
            return null;
        }

        //TODO: find better way to detect warcaster
        var affinities = rulesetCharacter.GetFeaturesByType<FeatureDefinitionMagicAffinity>();

        if (affinities.All(a => a.Name != "MagicAffinityWarCasterFeat"))
        {
            return null;
        }

        var cantrips = new List<SpellDefinition>();
        rulesetCharacter.EnumerateReadyAttackCantrips(cantrips);

        cantrips.RemoveAll(cantrip =>
        {
            if (cantrip.ActivationTime != RuleDefinitions.ActivationTime.Action
                && cantrip.ActivationTime != RuleDefinitions.ActivationTime.BonusAction)
            {
                return true;
            }

            var attackParams = new BattleDefinitions.AttackEvaluationParams();
            var actionModifier = new ActionModifier();

            attackParams.FillForMagic(character,
                character.LocationPosition,
                cantrip.EffectDescription,
                cantrip.Name,
                target,
                target.LocationPosition,
                actionModifier);

            return !battle.IsValidAttackForReadiedAction(attackParams, false);
        });

        return cantrips;
    }


    public override void SelectSubOption(int option)
    {
        ReactionParams.RulesetEffect?.Terminate(false);

        var targetCharacters = reactionParams.TargetCharacters;

        while (targetCharacters.Count > 1)
        {
            reactionParams.TargetCharacters.RemoveAt(targetCharacters.Count - 1);
            reactionParams.ActionModifiers.RemoveAt(reactionParams.ActionModifiers.Count - 1);
        }

        var actingCharacter = reactionParams.ActingCharacter;
        if (option == 0)
        {
            reactionParams.ActionDefinition = ServiceRepository.GetService<IGameLocationActionService>()
                .AllActionDefinitions[ActionDefinitions.Id.AttackOpportunity];
            reactionParams.RulesetEffect = null;
            var attackParams = new BattleDefinitions.AttackEvaluationParams();
            var actionModifier = new ActionModifier();
            attackParams.FillForPhysicalReachAttack(actingCharacter,
                actingCharacter.LocationPosition,
                reactionParams.AttackMode,
                reactionParams.TargetCharacters[0],
                reactionParams.TargetCharacters[0].LocationPosition, actionModifier);
            reactionParams.ActionModifiers[0] = actionModifier;
        }
        else
        {
            reactionParams.ActionDefinition = ServiceRepository.GetService<IGameLocationActionService>()
                .AllActionDefinitions[ActionDefinitions.Id.CastReaction];
            var spell = reactionParams.SpellRepertoire.KnownSpells[option - 1];
            var rulesService =
                ServiceRepository.GetService<IRulesetImplementationService>();
            var rulesetCharacter = actingCharacter.RulesetCharacter;
            rulesetCharacter.CanCastCantrip(spell, out var spellRepertoire);
            var spellEffect = rulesService.InstantiateEffectSpell(rulesetCharacter, spellRepertoire,
                spell, spell.SpellLevel, false);
            ReactionParams.RulesetEffect = spellEffect;

            var spelltargets = spellEffect.ComputeTargetParameter();

            if (!reactionParams.RulesetEffect.EffectDescription.IsSingleTarget || spelltargets <= 0)
            {
                return;
            }

            var target = reactionParams.TargetCharacters.FirstOrDefault();
            var mod = reactionParams.ActionModifiers.FirstOrDefault();

            while (target != null && mod != null && reactionParams.TargetCharacters.Count < spelltargets)
            {
                reactionParams.TargetCharacters.Add(target);
                // Technically casts after first might need to have different mods, but not by much since we attacking same target.
                reactionParams.ActionModifiers.Add(mod);
            }
        }
    }

    public override string FormatTitle()
    {
        return Gui.Localize($"Reaction/&{type}Title");
    }

    public override string FormatDescription()
    {
        return Gui.Format($"Reaction/&{type}Description", guiTarget.Name);
    }

    public override string FormatReactTitle()
    {
        return Gui.Localize($"Reaction/&{type}ReactTitle");
    }

    public override string FormatReactDescription()
    {
        return Gui.Localize($"Reaction/&{type}ReactDescription");
    }

    public override void OnSetInvalid()
    {
        base.OnSetInvalid();
        ReactionParams.RulesetEffect?.Terminate(false);
    }
}
