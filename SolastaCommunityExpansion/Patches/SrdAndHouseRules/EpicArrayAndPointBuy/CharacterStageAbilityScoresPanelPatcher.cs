﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using HarmonyLib;
using SolastaCommunityExpansion.Models;

namespace SolastaCommunityExpansion.Patches.SrdAndHouseRules.EpicArrayAndPointBuy;

// enables epic points
[HarmonyPatch(typeof(CharacterStageAbilityScoresPanel), "Reset")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class CharacterStageAbilityScoresPanel_Reset
{
    internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            if (Main.Settings.EnableEpicPointsAndArray)
            {
                if (instruction.opcode == OpCodes.Ldc_I4_S && instruction.operand.ToString() ==
                    InitialChoicesContext.GameBuyPoints.ToString())
                {
                    yield return new CodeInstruction(OpCodes.Ldc_I4_S, InitialChoicesContext.ModBuyPoints);
                }
                else
                {
                    yield return instruction;
                }
            }
            else
            {
                yield return instruction;
            }
        }
    }
}

// enables epic points
[HarmonyPatch(typeof(CharacterStageAbilityScoresPanel), "Refresh")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class CharacterStageAbilityScoresPanel_Refresh
{
    internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            if (Main.Settings.EnableEpicPointsAndArray)
            {
                if (instruction.opcode == OpCodes.Ldc_R4 && instruction.operand.ToString() ==
                    InitialChoicesContext.GameBuyPoints.ToString())
                {
                    yield return new CodeInstruction(OpCodes.Ldc_R4, 1f * InitialChoicesContext.ModBuyPoints);
                }
                else if (instruction.opcode == OpCodes.Ldc_I4_S && instruction.operand.ToString() ==
                         InitialChoicesContext.GameBuyPoints.ToString())
                {
                    yield return new CodeInstruction(OpCodes.Ldc_I4_S, InitialChoicesContext.ModBuyPoints);
                }
                else if (instruction.opcode == OpCodes.Ldc_I4_S && instruction.operand.ToString() ==
                         InitialChoicesContext.GameMaxAttribute.ToString())
                {
                    yield return new CodeInstruction(OpCodes.Ldc_I4_S, InitialChoicesContext.ModMaxAttribute);
                }
                else
                {
                    yield return instruction;
                }
            }
            else
            {
                yield return instruction;
            }
        }
    }
}
