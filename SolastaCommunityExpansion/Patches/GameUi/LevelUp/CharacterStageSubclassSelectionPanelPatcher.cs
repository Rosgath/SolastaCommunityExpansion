﻿using System;
using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace SolastaCommunityExpansion.Patches.GameUi.LevelUp;

[HarmonyPatch(typeof(CharacterStageSubclassSelectionPanel), "OnBeginShow")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class CharacterStageSubclassSelectionPanel_OnBeginShow
{
    private static Vector2 OriginalAnchoredPosition { get; set; } = Vector2.zero;

    internal static void Prefix([NotNull] CharacterStageSubclassSelectionPanel __instance)
    {
        if (Main.Settings.EnableSortingSubclasses)
        {
            __instance.compatibleSubclasses
                .Sort((a, b) =>
                    String.Compare(a.FormatTitle(), b.FormatTitle(), StringComparison.CurrentCultureIgnoreCase));
        }

        var gridLayoutGroup = __instance.subclassesTable.GetComponent<GridLayoutGroup>();
        var count = __instance.compatibleSubclasses.Count;

        if (OriginalAnchoredPosition == Vector2.zero)
        {
            OriginalAnchoredPosition = __instance.subclassesTable.anchoredPosition;
        }

        if (count > 8)
        {
            gridLayoutGroup.constraintCount = 3;
            //__instance.subclassesTable.anchoredPosition = new Vector2(0, +15);
            __instance.subclassesTable.localScale = new Vector3(0.8f, 0.8f, 1f);
        }
        else
        {
            gridLayoutGroup.constraintCount = 2;
            //__instance.subclassesTable.anchoredPosition = OriginalAnchoredPosition;
            __instance.subclassesTable.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
