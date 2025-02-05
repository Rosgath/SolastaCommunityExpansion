﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using JetBrains.Annotations;
using SolastaCommunityExpansion.Api.Extensions;
using SolastaCommunityExpansion.CustomUI;
using UnityEngine;

namespace SolastaCommunityExpansion.Patches.CustomFeatures.CustomReactions;

[HarmonyPatch(typeof(CharacterReactionItem), "Bind")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class CharacterReactionItem_Bind
{
    [NotNull]
    internal static IEnumerable<CodeInstruction> Transpiler([NotNull] IEnumerable<CodeInstruction> instructions)
    {
        var codes = instructions.ToList();
        var customBindMethod =
            new Action<CharacterReactionSubitem, RulesetSpellRepertoire, int, string, bool,
                CharacterReactionSubitem.SubitemSelectedHandler, ReactionRequest>(CustomBind).Method;

        var bind = typeof(CharacterReactionSubitem).GetMethod("Bind",
            BindingFlags.Public | BindingFlags.Instance);

        var bindIndex = codes.FindIndex(x => x.Calls(bind));

        if (bindIndex <= 0)
        {
            return codes.AsEnumerable();
        }

        codes[bindIndex] = new CodeInstruction(OpCodes.Call, customBindMethod);
        codes.Insert(bindIndex, new CodeInstruction(OpCodes.Ldarg_1));

        return codes.AsEnumerable();
    }

    internal static void Postfix([NotNull] CharacterReactionItem __instance)
    {
        var request = __instance.ReactionRequest;
        var size = request is ReactionRequestWarcaster or ReactionRequestSpendBundlePower
            ? 400
            : 290;

        __instance.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
    }

    private static void CustomBind(
        [NotNull] CharacterReactionSubitem instance,
        RulesetSpellRepertoire spellRepertoire,
        int slotLevel,
        string text,
        bool interactable,
        CharacterReactionSubitem.SubitemSelectedHandler subitemSelected,
        ReactionRequest reactionRequest)
    {
        switch (reactionRequest)
        {
            case ReactionRequestWarcaster warcasterRequest:
                instance.BindWarcaster(warcasterRequest, slotLevel, interactable, subitemSelected);
                break;
            case ReactionRequestSpendBundlePower bundlePowerRequest:
                instance.BindPowerBundle(bundlePowerRequest, slotLevel, interactable, subitemSelected);
                break;
            default:
                instance.Bind(spellRepertoire, slotLevel, text, interactable, subitemSelected);
                break;
        }
    }
}

// Replace `GetSelectedSubItem` to fix reaction selection crashes.
// Default one selects last item that is Selected, regardless if it is active or not, leading to wrong spell slots for smites being selected
// This implementation returns first item that is both Selected and active
[HarmonyPatch(typeof(CharacterReactionItem), "GetSelectedSubItem")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class CharacterReactionItem_GetSelectedSubItem
{
    internal static bool Prefix([NotNull] CharacterReactionItem __instance, out int __result)
    {
        __result = 0;

        var itemsTable = __instance.subItemsTable;

        for (var index = 0; index < itemsTable.childCount; ++index)
        {
            var item = itemsTable.GetChild(index).GetComponent<CharacterReactionSubitem>();

            if (!item.gameObject.activeSelf || !item.Selected)
            {
                continue;
            }

            __result = index;
            break;
        }

        return false;
    }
}
