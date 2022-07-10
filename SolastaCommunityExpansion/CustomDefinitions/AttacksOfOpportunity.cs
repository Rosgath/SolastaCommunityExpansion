﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SolastaCommunityExpansion.Api;
using SolastaCommunityExpansion.Api.Extensions;
using SolastaCommunityExpansion.CustomUI;
using SolastaCommunityExpansion.Feats;
using SolastaCommunityExpansion.Models;
using TA;

namespace SolastaCommunityExpansion.CustomDefinitions;

public interface ICanIgnoreAoOImmunity
{
    bool CanIgnoreAoOImmunity(RulesetCharacter character, RulesetCharacter attacker);
}

public static class AttacksOfOpportunity
{
    public const string NotAoOTag = "NotAoO"; //Used to distinguish reaction attacks from AoO
    public static readonly ICanIgnoreAoOImmunity CanIgnoreDisengage = new CanIgnoreDisengage();
    public static readonly object SentinelFeatMarker = new SentinelFeatMarker();
    private static readonly Dictionary<ulong, (int3, int3)> movingCharactersCache = new();

    public static IEnumerator ProcessOnCharacterAttackFinished(
        GameLocationBattleManager battleManager,
        GameLocationCharacter attacker,
        GameLocationCharacter defender,
        RulesetAttackMode attackerAttackMode)
    {
        if (battleManager == null)
        {
            yield break;
        }

        var battle = battleManager.Battle;
        if (battle == null)
        {
            yield break;
        }

        //Process features on attacker or defender

        var units = battle.AllContenders
            .Where(u => !u.RulesetCharacter.IsDeadOrDyingOrUnconscious)
            .ToArray();

        //Process other participants of the battle
        foreach (var unit in units)
        {
            if (attacker != unit && defender != unit)
            {
                yield return ProcessSentinel(unit, attacker, defender, battleManager);
            }
        }
    }

    private static IEnumerator ProcessSentinel(GameLocationCharacter unit, GameLocationCharacter attacker,
        GameLocationCharacter defender, GameLocationBattleManager battleManager)
    {
        if (attacker.IsOppositeSide(unit.Side)
            && defender.Side == unit.Side
            && (unit.RulesetCharacter?.HasSubFeatureOfType<SentinelFeatMarker>() ?? false)
            && !(defender.RulesetCharacter?.HasSubFeatureOfType<SentinelFeatMarker>() ?? false)
            && CanMakeAoO(unit, attacker, out var opportunityAttackMode, out var actionModifier,
                battleManager))
        {
            var actionService = ServiceRepository.GetService<IGameLocationActionService>();
            var count = actionService.PendingReactionRequestGroups.Count;

            RequestReactionAttack(EwFeats.SentinelFeat, new CharacterActionParams(
                unit,
                ActionDefinitions.Id.AttackOpportunity,
                opportunityAttackMode,
                attacker,
                actionModifier)
            );

            yield return battleManager.WaitForReactions(unit, actionService, count);
        }
    }

    public static void ProcessOnCharacterMoveStart(GameLocationCharacter mover, int3 destination)
    {
        movingCharactersCache.AddOrReplace(mover.Guid, (mover.locationPosition, destination));
    }

    public static IEnumerator ProcessOnCharacterMoveEnd(GameLocationBattleManager battleManager,
        GameLocationCharacter mover)
    {
        if (battleManager == null)
        {
            yield break;
        }

        var battle = battleManager.Battle;
        if (battle == null)
        {
            yield break;
        }

        var units = battle.AllContenders
            .Where(u => !u.RulesetCharacter.IsDeadOrDyingOrUnconscious)
            .ToArray();

        //Process other participants of the battle
        foreach (var unit in units)
        {
            if (mover != unit)
            {
                yield return ProcessPolearmExpert(unit, mover, battleManager);
            }
        }
    }

    public static void CleanMovingCache()
    {
        movingCharactersCache.Clear();
    }

    private static IEnumerator ProcessPolearmExpert(GameLocationCharacter attacker, GameLocationCharacter mover,
        GameLocationBattleManager battleManager)
    {
        if (attacker.IsOppositeSide(mover.Side)
            && CanMakeAoOOnEnemyEnterReach(attacker.RulesetCharacter)
            && movingCharactersCache.TryGetValue(mover.Guid, out var movement)
            && battleManager.CanPerformOpportunityAttackOnCharacter(attacker, mover, movement.Item2, movement.Item1,
                out var attackMode))
        {
            var actionService = ServiceRepository.GetService<IGameLocationActionService>();
            var count = actionService.PendingReactionRequestGroups.Count;

            actionService.ReactForOpportunityAttack(new CharacterActionParams(
                attacker,
                ActionDefinitions.Id.AttackOpportunity,
                attackMode,
                mover,
                new ActionModifier()));

            yield return battleManager.WaitForReactions(attacker, actionService, count);
        }
    }

    private static bool CanMakeAoOOnEnemyEnterReach(RulesetCharacter character)
    {
        return character != null &&
               character.GetSubFeaturesByType<CanmakeAoOOnReachEntered>()
                   .Any(f => f.IsValid(character));
    }

    public static void RequestReactionAttack(string type, CharacterActionParams actionParams)
    {
        var actionManager = ServiceRepository.GetService<IGameLocationActionService>() as GameLocationActionManager;
        if (actionManager != null)
        {
            actionParams.AttackMode?.AddAttackTagAsNeeded(NotAoOTag);
            actionManager.AddInterruptRequest(new ReactionRequestReactionAttack(type, actionParams));
        }
    }

    private static bool CanMakeAoO(GameLocationCharacter attacker, GameLocationCharacter defender,
        out RulesetAttackMode attackMode, out ActionModifier actionModifier,
        IGameLocationBattleService battleManager = null)
    {
        battleManager ??= ServiceRepository.GetService<IGameLocationBattleService>();

        actionModifier = new ActionModifier();
        attackMode = null;
        if (!battleManager.IsValidAttackerForOpportunityAttackOnCharacter(attacker, defender))
        {
            return false;
        }

        attackMode = attacker.FindActionAttackMode(ActionDefinitions.Id.AttackOpportunity);
        if (attackMode == null)
        {
            return false;
        }

        var evaluationParams = new BattleDefinitions.AttackEvaluationParams();
        evaluationParams.FillForPhysicalReachAttack(attacker, attacker.LocationPosition, attackMode, defender,
            defender.LocationPosition, actionModifier);
        return battleManager.CanAttack(evaluationParams);
    }

    public static bool IsSubjectToAttackOfOpportunity(RulesetCharacter character, RulesetCharacter attacker,
        bool def)
    {
        return def || attacker.GetSubFeaturesByType<ICanIgnoreAoOImmunity>()
            .Any(f => f.CanIgnoreAoOImmunity(character, attacker));
    }
}

internal class CanIgnoreDisengage : ICanIgnoreAoOImmunity
{
    public bool CanIgnoreAoOImmunity(RulesetCharacter character, RulesetCharacter attacker)
    {
        var FeaturesToBrowse = new List<FeatureDefinition>();
        character.EnumerateFeaturesToBrowse<ICombatAffinityProvider>(FeaturesToBrowse);
        var service = ServiceRepository.GetService<IRulesetImplementationService>();
        var disengaging = DatabaseHelper.FeatureDefinitionCombatAffinitys.CombatAffinityDisengaging;
        foreach (var feature in FeaturesToBrowse)
        {
            if (feature != disengaging
                && feature is ICombatAffinityProvider affinityProvider
                && service.IsSituationalContextValid(
                    new RulesetImplementationDefinitions.SituationalContextParams(
                        affinityProvider.SituationalContext, attacker,
                        character, service.FindSourceIdOfFeature(character, feature),
                        affinityProvider.RequiredCondition, null))
                && affinityProvider.IsImmuneToOpportunityAttack(character, attacker))
            {
                return false;
            }
        }

        return true;
    }
}

internal class SentinelFeatMarker
{
}

internal class CanmakeAoOOnReachEntered
{
    private readonly CharacterValidator[] validators;

    public CanmakeAoOOnReachEntered(params CharacterValidator[] validators)
    {
        this.validators = validators;
    }

    public bool IsValid(RulesetCharacter character)
    {
        return character != null && character.IsValid(validators);
    }
}

public interface IReactToAttackFinished
{
    IEnumerator HandleReactToAttackFinished(GameLocationCharacter character, GameLocationCharacter defender,
        RuleDefinitions.RollOutcome outcome, CharacterActionParams actionParams, RulesetAttackMode mode,
        ActionModifier modifier);
}

public delegate IEnumerator ReactToAttackFinishedHandler(GameLocationCharacter character,
    GameLocationCharacter defender, RuleDefinitions.RollOutcome outcome,
    CharacterActionParams actionParams, RulesetAttackMode mode, ActionModifier modifier);

public class ReactToAttackFinished : IReactToAttackFinished
{
    private readonly ReactToAttackFinishedHandler handler;

    public ReactToAttackFinished(ReactToAttackFinishedHandler handler)
    {
        this.handler = handler;
    }

    public IEnumerator HandleReactToAttackFinished(GameLocationCharacter character, GameLocationCharacter defender,
        RuleDefinitions.RollOutcome outcome,
        CharacterActionParams actionParams, RulesetAttackMode mode, ActionModifier modifier)
    {
        yield return handler(character, defender, outcome, actionParams, mode, modifier);
    }
}
