﻿using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using HarmonyLib;
using JetBrains.Annotations;
using SolastaCommunityExpansion.Api.Extensions;
using SolastaCommunityExpansion.CustomInterfaces;

namespace SolastaCommunityExpansion.Patches.CustomFeatures.OnCharacterAttackEffect;

//
// this patch shouldn't be protected
//
[HarmonyPatch(typeof(GameLocationActionManager), "ExecuteActionAsync")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class GameLocationActionManager_ExecuteActionAsync
{
    internal static IEnumerator Postfix(
        [NotNull] IEnumerator values,
        [NotNull] CharacterAction action)
    {
        Main.Logger.Log(action.ActionDefinition.Name);

        var features = action.ActingCharacter.RulesetCharacter.GetSubFeaturesByType<ICustomOnActionFeature>();

        foreach (var feature in features)
        {
            feature.OnBeforeAction(action);
        }

        while (values.MoveNext())
        {
            yield return values.Current;
        }

        foreach (var feature in features)
        {
            feature.OnAfterAction(action);
        }
    }
}

// Make cantrips that have more than 1 target hit same target more than once when used as readied action.
// Only Eldritch Blast and its variants should be affected
[HarmonyPatch(typeof(GameLocationActionManager), "ReactForReadiedAction")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class GameLocationActionManager_ReactForReadiedAction
{
    internal static bool Prefix([NotNull] CharacterActionParams reactionParams)
    {
        // For some reason TA do not set reactionParams.ReadyActionType to ReadyActionType.Cantrip
        // So we manually detect it as casting spell level 0
        if (reactionParams.RulesetEffect is not RulesetEffectSpell {SlotLevel: 0} spell)
        {
            return true;
        }

        var spellTargets = spell.ComputeTargetParameter();

        if (!reactionParams.RulesetEffect.EffectDescription.IsSingleTarget || spellTargets <= 1)
        {
            return true;
        }

        var target = reactionParams.TargetCharacters.FirstOrDefault();
        var mod = reactionParams.ActionModifiers.FirstOrDefault();

        while (target != null && mod != null && reactionParams.TargetCharacters.Count < spellTargets)
        {
            reactionParams.TargetCharacters.Add(target);
            // Technically casts after first might need to have different mods, but not by much since we attacking same target.
            reactionParams.ActionModifiers.Add(mod);
        }

        return true;
    }
}
