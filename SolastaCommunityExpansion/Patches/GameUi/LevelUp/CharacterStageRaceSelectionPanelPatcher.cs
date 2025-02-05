﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using HarmonyLib;
using SolastaCommunityExpansion.Api.Infrastructure;

namespace SolastaCommunityExpansion.Patches.GameUi.LevelUp;

[HarmonyPatch(typeof(CharacterStageRaceSelectionPanel), "Compare")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class CharacterStageRaceSelectionPanel_Compare
{
    internal static void Postfix(CharacterRaceDefinition left, CharacterRaceDefinition right, ref int __result)
    {
        if (!Main.Settings.EnableSortingRaces)
        {
            return;
        }

        __result = String.Compare(left.FormatTitle(), right.FormatTitle(), StringComparison.CurrentCultureIgnoreCase);
    }
}

// avoids a restart when enabling / disabling races on the Mod UI panel
[HarmonyPatch(typeof(CharacterStageRaceSelectionPanel), "OnBeginShow")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class CharacterStageRaceSelectionPanel_OnBeginShow
{
    internal static void Prefix(CharacterStageRaceSelectionPanel __instance)
    {
        var visibleRaces = DatabaseRepository.GetDatabase<CharacterRaceDefinition>()
            .Where(x => !x.GuiPresentation.Hidden);
        var characterRaceDefinitions = visibleRaces as CharacterRaceDefinition[] ?? visibleRaces.ToArray();
        var visibleSubRaces = characterRaceDefinitions.SelectMany(x => x.SubRaces);
        var visibleMainRaces = characterRaceDefinitions.Where(x => !visibleSubRaces.Contains(x));

        var raceDefinitions = visibleMainRaces as CharacterRaceDefinition[] ?? visibleMainRaces.ToArray();
        __instance.eligibleRaces.SetRange(raceDefinitions.OrderBy(x => x.FormatTitle()));
        __instance.selectedSubRace.Clear();

        for (var key = 0; key < raceDefinitions.Length; ++key)
        {
            __instance.selectedSubRace[key] = 0;
        }
    }
}
