﻿using HarmonyLib;
using System.Diagnostics.CodeAnalysis;

namespace SolastaCommunityExpansion.Patches
{
    [HarmonyPatch(typeof(GuiPanel), "Show")]
    [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
    internal static class GuiPanel_Show
    {
        internal static void Postfix(GuiPanel __instance)
        {
            if (__instance is MainMenuScreen mainMenuScreen && CharacterEditionScreen_OnFinishCb.HeroName != null)
            {
                var charactersPanel = AccessTools.Field(mainMenuScreen.GetType(), "charactersPanel").GetValue(mainMenuScreen) as CharactersPanel;

                charactersPanel.Show();
            }
        }
    }
}
