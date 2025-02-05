﻿using System;
using JetBrains.Annotations;
using SolastaCommunityExpansion.Api.Infrastructure;
using SolastaCommunityExpansion.Builders;
using SolastaCommunityExpansion.Builders.Features;
using SolastaCommunityExpansion.Models;
using SolastaCommunityExpansion.Properties;
using SolastaCommunityExpansion.Utils;
using TA;
using static SolastaCommunityExpansion.Api.DatabaseHelper;
using static SolastaCommunityExpansion.Api.DatabaseHelper.CharacterRaceDefinitions;

namespace SolastaCommunityExpansion.Races;

internal static class GrayDwarfSubraceBuilder
{
    private static readonly Guid GrayDwarfNamespace = new("6dcf3e31-8c94-44e4-9dda-8eee0edf21d5");

    internal static CharacterRaceDefinition GrayDwarfSubrace { get; } = BuildGrayDwarf();

    [NotNull]
    private static CharacterRaceDefinition BuildGrayDwarf()
    {
        var grayDwarfSpriteReference =
            CustomIcons.CreateAssetReferenceSprite("GrayDwarf", Resources.GrayDwarf, 1024, 512);
        //Dwarf.GuiPresentation.SpriteReference;

        var grayDwarfAbilityScoreModifierStrength = FeatureDefinitionAttributeModifierBuilder
            .Create("AttributeModifierGrayDwarfStrengthAbilityScoreIncrease", GrayDwarfNamespace)
            .SetGuiPresentation(Category.Feature)
            .SetModifier(FeatureDefinitionAttributeModifier.AttributeModifierOperation.Additive,
                AttributeDefinitions.Strength, 1)
            .AddToDB();

        var grayDwarfPerceptionLightSensitivity = FeatureDefinitionAbilityCheckAffinityBuilder
            .Create("AbilityCheckAffinityGrayDwarfLightSensitivity", GrayDwarfNamespace)
            .SetGuiPresentation(Category.Feature)
            .BuildAndSetAffinityGroups(
                RuleDefinitions.CharacterAbilityCheckAffinity.Disadvantage, RuleDefinitions.DieType.D1, 0,
                (AttributeDefinitions.Wisdom, SkillDefinitions.Perception))
            .AddToDB();

        grayDwarfPerceptionLightSensitivity.AffinityGroups[0].lightingContext =
            RuleDefinitions.LightingContext.BrightLight;

        var grayDwarfCombatAffinityLightSensitivity = FeatureDefinitionCombatAffinityBuilder
            .Create(FeatureDefinitionCombatAffinitys.CombatAffinitySensitiveToLight,
                "CombatAffinityGrayDwarfLightSensitivity", GrayDwarfNamespace)
            .SetGuiPresentation(
                "Feature/&LightAffinityGrayDwarfLightSensitivityTitle",
                "Feature/&LightAffinityGrayDwarfLightSensitivityDescription")
            .SetMyAttackAdvantage(RuleDefinitions.AdvantageType.Disadvantage)
            .SetMyAttackModifierSign(RuleDefinitions.AttackModifierSign.Substract)
            .SetMyAttackModifierDieType(RuleDefinitions.DieType.D4)
            .AddToDB();

        var grayDwarfConditionLightSensitive = ConditionDefinitionBuilder
            .Create("ConditionGrayDwarfLightSensitive", GrayDwarfNamespace)
            .SetGuiPresentation(
                "Feature/&LightAffinityGrayDwarfLightSensitivityTitle",
                "Feature/&LightAffinityGrayDwarfLightSensitivityDescription",
                ConditionDefinitions.ConditionLightSensitive.GuiPresentation.SpriteReference)
            .SetSilent(Silent.WhenAddedOrRemoved)
            .SetPossessive(true)
            .SetConditionType(RuleDefinitions.ConditionType.Detrimental)
            .SetFeatures(grayDwarfPerceptionLightSensitivity, grayDwarfCombatAffinityLightSensitivity)
            .AddToDB();

        Global.CharacterLabelEnabledConditions.Add(grayDwarfConditionLightSensitive);

        var grayDwarfLightingEffectAndCondition = new FeatureDefinitionLightAffinity.LightingEffectAndCondition
        {
            lightingState = LocationDefinitions.LightingState.Bright, condition = grayDwarfConditionLightSensitive
        };

        var grayDwarfLightAffinity = FeatureDefinitionLightAffinityBuilder
            .Create("LightAffinityGrayDwarfLightSensitivity", GrayDwarfNamespace)
            .SetGuiPresentation(Category.Feature)
            .AddLightingEffectAndCondition(grayDwarfLightingEffectAndCondition)
            .AddToDB();

        if (Main.Settings.ReduceGrayDwarfLightPenalty)
        {
            const string REDUCED_DESCRIPTION = "Feature/&LightAffinityGrayDwarfReducedLightSensitivityDescription";

            grayDwarfCombatAffinityLightSensitivity.myAttackAdvantage = RuleDefinitions.AdvantageType.None;
            grayDwarfCombatAffinityLightSensitivity.myAttackModifierValueDetermination =
                RuleDefinitions.CombatAddinityValueDetermination.Die;
            grayDwarfCombatAffinityLightSensitivity.GuiPresentation.description = REDUCED_DESCRIPTION;
            grayDwarfConditionLightSensitive.GuiPresentation.description = REDUCED_DESCRIPTION;
            grayDwarfLightAffinity.GuiPresentation.description = REDUCED_DESCRIPTION;
        }
        else
        {
            grayDwarfCombatAffinityLightSensitivity.myAttackAdvantage = RuleDefinitions.AdvantageType.Disadvantage;
            grayDwarfCombatAffinityLightSensitivity.myAttackModifierValueDetermination =
                RuleDefinitions.CombatAddinityValueDetermination.None;
            grayDwarfCombatAffinityLightSensitivity.GuiPresentation.description =
                grayDwarfLightAffinity.GuiPresentation.Description;
            grayDwarfConditionLightSensitive.GuiPresentation.description =
                grayDwarfLightAffinity.GuiPresentation.Description;
            grayDwarfLightAffinity.GuiPresentation.description = grayDwarfLightAffinity.GuiPresentation.Description;
        }

        var grayDwarfConditionAffinityGrayDwarfCharm = FeatureDefinitionConditionAffinityBuilder
            .Create(FeatureDefinitionConditionAffinitys.ConditionAffinityElfFeyAncestryCharm,
                "ConditionAffinityGrayDwarfCharm", GrayDwarfNamespace)
            .SetGuiPresentationNoContent()
            .AddToDB();

        var grayDwarfConditionAffinityGrayDwarfCharmedByHypnoticPattern = FeatureDefinitionConditionAffinityBuilder
            .Create(FeatureDefinitionConditionAffinitys.ConditionAffinityElfFeyAncestryCharmedByHypnoticPattern,
                "ConditionAffinityGrayDwarfCharmedByHypnoticPattern", GrayDwarfNamespace)
            .SetGuiPresentationNoContent()
            .AddToDB();

        var grayDwarfConditionAffinityGrayDwarfParalyzedAdvantage = FeatureDefinitionConditionAffinityBuilder
            .Create(FeatureDefinitionConditionAffinitys.ConditionAffinityHalflingBrave,
                "ConditionAffinityGrayDwarfParalyzedAdvantage", GrayDwarfNamespace)
            .SetConditionType(ConditionDefinitions.ConditionParalyzed)
            .AddToDB();

        var grayDwarfSavingThrowAffinityGrayDwarfIllusion = FeatureDefinitionSavingThrowAffinityBuilder
            .Create(FeatureDefinitionSavingThrowAffinitys.SavingThrowAffinityGemIllusion,
                "SavingThrowAffinityGrayDwarfIllusion", GrayDwarfNamespace)
            .AddToDB();

        grayDwarfSavingThrowAffinityGrayDwarfIllusion.AffinityGroups[0].affinity =
            RuleDefinitions.CharacterSavingThrowAffinity.Advantage;
        grayDwarfSavingThrowAffinityGrayDwarfIllusion.AffinityGroups[0].savingThrowModifierDiceNumber = 0;
        grayDwarfSavingThrowAffinityGrayDwarfIllusion.AffinityGroups[1].affinity =
            RuleDefinitions.CharacterSavingThrowAffinity.Advantage;
        grayDwarfSavingThrowAffinityGrayDwarfIllusion.AffinityGroups[1].savingThrowModifierDiceNumber = 0;
        grayDwarfSavingThrowAffinityGrayDwarfIllusion.AffinityGroups[2].affinity =
            RuleDefinitions.CharacterSavingThrowAffinity.Advantage;
        grayDwarfSavingThrowAffinityGrayDwarfIllusion.AffinityGroups[2].savingThrowModifierDiceNumber = 0;
        grayDwarfSavingThrowAffinityGrayDwarfIllusion.AffinityGroups[3].affinity =
            RuleDefinitions.CharacterSavingThrowAffinity.Advantage;
        grayDwarfSavingThrowAffinityGrayDwarfIllusion.AffinityGroups[3].savingThrowModifierDiceNumber = 0;
        grayDwarfSavingThrowAffinityGrayDwarfIllusion.AffinityGroups[4].affinity =
            RuleDefinitions.CharacterSavingThrowAffinity.Advantage;
        grayDwarfSavingThrowAffinityGrayDwarfIllusion.AffinityGroups[4].savingThrowModifierDiceNumber = 0;
        grayDwarfSavingThrowAffinityGrayDwarfIllusion.AffinityGroups[5].affinity =
            RuleDefinitions.CharacterSavingThrowAffinity.Advantage;
        grayDwarfSavingThrowAffinityGrayDwarfIllusion.AffinityGroups[5].savingThrowModifierDiceNumber = 0;

        var grayDwarfAncestryFeatureSetGrayDwarfAncestry = FeatureDefinitionFeatureSetBuilder
            .Create(FeatureDefinitionFeatureSets.FeatureSetElfFeyAncestry, "FeatureSetGrayDwarfAncestry",
                GrayDwarfNamespace)
            .SetGuiPresentation(Category.Feature)
            .SetFeatureSet(
                grayDwarfConditionAffinityGrayDwarfCharm,
                grayDwarfConditionAffinityGrayDwarfCharmedByHypnoticPattern,
                grayDwarfConditionAffinityGrayDwarfParalyzedAdvantage,
                grayDwarfSavingThrowAffinityGrayDwarfIllusion)
            .AddToDB();

        var grayDwarfAbilityCheckAffinityConditionStoneStrength = FeatureDefinitionAbilityCheckAffinityBuilder
            .Create(FeatureDefinitionAbilityCheckAffinitys.AbilityCheckAffinityConditionBullsStrength,
                "AbilityCheckAffinityConditionStoneStrength", GrayDwarfNamespace)
            .AddToDB();

        var grayDwarfSavingThrowAffinityConditionStoneStrength = FeatureDefinitionSavingThrowAffinityBuilder
            .Create(FeatureDefinitionSavingThrowAffinitys.SavingThrowAffinityConditionRaging,
                "SavingThrowAffinityConditionStoneStrength", GrayDwarfNamespace)
            .AddToDB();

        var grayDwarfAdditionalDamageConditionStoneStrength = FeatureDefinitionAdditionalDamageBuilder
            .Create("AdditionalDamageConditionStoneStrength", GrayDwarfNamespace)
            .SetGuiPresentationNoContent()
            .SetNotificationTag("StoneStrength")
            .SetTriggerCondition(RuleDefinitions.AdditionalDamageTriggerCondition.AlwaysActive)
            .SetRequiredProperty(RuleDefinitions.AdditionalDamageRequiredProperty.MeleeStrengthWeapon)
            .SetDamageDice(RuleDefinitions.DieType.D4, 1)
            .SetDamageValueDetermination(RuleDefinitions.AdditionalDamageValueDetermination.Die)
            .SetAdditionalDamageType(RuleDefinitions.AdditionalDamageType.SameAsBaseDamage)
            .SetFrequencyLimit(RuleDefinitions.FeatureLimitedUsage.None)
            .AddToDB();

        var grayDwarfConditionStoneStrength = ConditionDefinitionBuilder
            .Create(ConditionDefinitions.ConditionBullsStrength, "ConditionStoneStrength", GrayDwarfNamespace)
            .SetGuiPresentation(
                Category.Condition,
                ConditionDefinitions.ConditionStoneResilience.GuiPresentation.SpriteReference)
            .SetFeatures(
                grayDwarfAbilityCheckAffinityConditionStoneStrength,
                grayDwarfSavingThrowAffinityConditionStoneStrength,
                grayDwarfAdditionalDamageConditionStoneStrength)
            .AddToDB();

        Global.CharacterLabelEnabledConditions.Add(grayDwarfConditionStoneStrength);

        var grayDwarfStoneStrengthEffect = EffectDescriptionBuilder
            .Create(SpellDefinitions.EnhanceAbilityBullsStrength.EffectDescription)
            .SetDurationData(RuleDefinitions.DurationType.Minute, 1, RuleDefinitions.TurnOccurenceType.StartOfTurn)
            .SetTargetingData(
                RuleDefinitions.Side.Ally,
                RuleDefinitions.RangeType.Self, 1,
                RuleDefinitions.TargetType.Self)
            .Build();

        grayDwarfStoneStrengthEffect.EffectForms[0].ConditionForm.conditionDefinition = grayDwarfConditionStoneStrength;

        var grayDwarfStoneStrengthPower = FeatureDefinitionPowerBuilder
            .Create("PowerGrayDwarfStoneStrength", GrayDwarfNamespace)
            .SetGuiPresentation(Category.Feature, SpellDefinitions.Stoneskin.GuiPresentation.SpriteReference)
            .SetEffectDescription(grayDwarfStoneStrengthEffect)
            .SetActivationTime(RuleDefinitions.ActivationTime.BonusAction)
            .SetFixedUsesPerRecharge(1)
            .SetRechargeRate(RuleDefinitions.RechargeRate.ShortRest)
            .SetCostPerUse(1)
            .SetShowCasting(true)
            .AddToDB();

        var grayDwarfInvisibilityEffect = EffectDescriptionBuilder
            .Create(SpellDefinitions.Invisibility.EffectDescription)
            .SetDurationData(RuleDefinitions.DurationType.Minute, 1, RuleDefinitions.TurnOccurenceType.StartOfTurn)
            .SetTargetingData(
                RuleDefinitions.Side.Ally,
                RuleDefinitions.RangeType.Self, 1,
                RuleDefinitions.TargetType.Self)
            .Build();

        var grayDwarfInvisibilityPower = FeatureDefinitionPowerBuilder
            .Create("PowerGrayDwarfInvisibility", GrayDwarfNamespace)
            .SetGuiPresentation(Category.Feature, SpellDefinitions.Invisibility.GuiPresentation.SpriteReference)
            .SetEffectDescription(grayDwarfInvisibilityEffect)
            .SetActivationTime(RuleDefinitions.ActivationTime.Action)
            .SetFixedUsesPerRecharge(1)
            .SetRechargeRate(RuleDefinitions.RechargeRate.ShortRest)
            .SetCostPerUse(1)
            .SetShowCasting(true)
            .AddToDB();

        var grayDwarfRacePresentation = Dwarf.RacePresentation.DeepCopy();

        grayDwarfRacePresentation.femaleNameOptions = DwarfHill.RacePresentation.FemaleNameOptions;
        grayDwarfRacePresentation.maleNameOptions = DwarfHill.RacePresentation.MaleNameOptions;
        grayDwarfRacePresentation.needBeard = false;
        grayDwarfRacePresentation.MaleBeardShapeOptions.SetRange(MorphotypeElementDefinitions.BeardShape_None.Name);
        grayDwarfRacePresentation.preferedSkinColors = new RangedInt(48, 53);
        grayDwarfRacePresentation.preferedHairColors = new RangedInt(35, 41);

        var grayDwarf = CharacterRaceDefinitionBuilder
            .Create(DwarfHill, "GrayDwarfRace", GrayDwarfNamespace)
            .SetGuiPresentation(Category.Race, grayDwarfSpriteReference)
            .SetRacePresentation(grayDwarfRacePresentation)
            .SetFeaturesAtLevel(1,
                FeatureDefinitionMoveModes.MoveModeMove5,
                FeatureDefinitionSenses.SenseSuperiorDarkvision,
                FeatureDefinitionProficiencys.ProficiencyDwarfLanguages,
                grayDwarfAncestryFeatureSetGrayDwarfAncestry,
                grayDwarfAbilityScoreModifierStrength,
                grayDwarfLightAffinity)
            .AddFeaturesAtLevel(3, grayDwarfStoneStrengthPower)
            .AddFeaturesAtLevel(5, grayDwarfInvisibilityPower)
            .AddToDB();

        grayDwarf.subRaces.Clear();
        Dwarf.SubRaces.Add(grayDwarf);

        return grayDwarf;
    }
}
