﻿using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;

namespace SolastaCommunityExpansion.Patches.Tools.DefaultParty;

[HarmonyPatch(typeof(CharacterSelectionModal), "EnumeratePlates")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class CharacterSelectionModal_EnumeratePlates
{
    internal static void Postfix([NotNull] CharacterSelectionModal __instance)
    {
        CharactersPanel_EnumeratePlates.Disable(__instance.charactersTable);
    }
}
