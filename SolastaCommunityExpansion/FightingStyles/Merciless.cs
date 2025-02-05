﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SolastaCommunityExpansion.Api;
using SolastaCommunityExpansion.Api.Extensions;
using SolastaCommunityExpansion.Builders;
using SolastaCommunityExpansion.Builders.Features;
using SolastaCommunityExpansion.CustomDefinitions;
using SolastaCommunityExpansion.Models;
using TA;
using static SolastaCommunityExpansion.Api.DatabaseHelper.FeatureDefinitionAdditionalActions;
using static SolastaCommunityExpansion.Api.DatabaseHelper.FeatureDefinitionFightingStyleChoices;

namespace SolastaCommunityExpansion.FightingStyles;

internal sealed class Merciless : AbstractFightingStyle
{
    private static FeatureDefinitionPower _powerMerciless;
    private readonly Guid guidNamespace = new("3f7f25de-0ff9-4b63-b38d-8cd7f3a401fc");
    private FightingStyleDefinitionCustomizable instance;

    [NotNull]
    internal override List<FeatureDefinitionFightingStyleChoice> GetChoiceLists()
    {
        return new List<FeatureDefinitionFightingStyleChoice>
        {
            FightingStyleChampionAdditional, FightingStyleFighter, FightingStylePaladin, FightingStyleRanger
        };
    }

    internal override FightingStyleDefinition GetStyle()
    {
        if (instance != null)
        {
            return instance;
        }

        _powerMerciless = FeatureDefinitionPowerBuilder
            .Create("PowerMerciless", guidNamespace)
            .SetGuiPresentation("Fear", Category.Spell)
            .Configure(
                1,
                RuleDefinitions.UsesDetermination.ProficiencyBonus,
                AttributeDefinitions.Strength,
                RuleDefinitions.ActivationTime.NoCost,
                0,
                RuleDefinitions.RechargeRate.AtWill,
                false,
                false,
                AttributeDefinitions.Strength,
                DatabaseHelper.SpellDefinitions.Fear.EffectDescription.Copy())
            .AddToDB();

        _powerMerciless.effectDescription.targetParameter = 1;
        _powerMerciless.effectDescription.TargetType = RuleDefinitions.TargetType.IndividualsUnique;
        _powerMerciless.effectDescription.durationType = RuleDefinitions.DurationType.Round;
        _powerMerciless.effectDescription.effectForms[0].canSaveToCancel = false;

        var additionalActionMerciless = FeatureDefinitionAdditionalActionBuilder
            .Create(AdditionalActionHunterHordeBreaker, "AdditionalActionMerciless", guidNamespace)
            .SetGuiPresentationNoContent()
            .AddToDB();

        var onCharacterKillMerciless = FeatureDefinitionOnCharacterKillBuilder
            .Create("FeatureMerciless", guidNamespace)
            .SetGuiPresentationNoContent()
            .SetOnCharacterKill(OnMercilessKill)
            .AddToDB();

        instance = CustomizableFightingStyleBuilder
            .Create("Merciless", "f570d166-c65c-4a68-ab78-aeb16d491fce")
            .SetGuiPresentation(Category.FightingStyle,
                DatabaseHelper.CharacterSubclassDefinitions.MartialChampion.GuiPresentation.SpriteReference)
            .SetFeatures(additionalActionMerciless, onCharacterKillMerciless)
            .AddToDB();

        return instance;
    }

    private static void OnMercilessKill(GameLocationCharacter character)
    {
        if (Global.CurrentAction is not CharacterActionAttack actionAttack)
        {
            return;
        }

        var battle = ServiceRepository.GetService<IGameLocationBattleService>()?.Battle;

        if (battle == null)
        {
            return;
        }

        var attacker = actionAttack.ActingCharacter.RulesetCharacter as RulesetCharacterHero
                       ?? actionAttack.ActingCharacter.RulesetCharacter.OriginalFormCharacter as
                           RulesetCharacterHero;

        if (attacker == null || attacker.IsWieldingRangedWeapon())
        {
            return;
        }

        var proficiencyBonus = attacker.GetAttribute(AttributeDefinitions.ProficiencyBonus).CurrentValue;
        var strength = attacker.GetAttribute(AttributeDefinitions.Strength).CurrentValue;
        var distance = Global.CriticalHit ? proficiencyBonus : (proficiencyBonus + 1) / 2;
        var usablePower = new RulesetUsablePower(_powerMerciless, attacker.RaceDefinition, attacker.ClassesHistory[0]);
        var effectPower = new RulesetEffectPower(attacker, usablePower);

        usablePower.SaveDC = 8 + proficiencyBonus + AttributeDefinitions.ComputeAbilityScoreModifier(strength);

        foreach (var enemy in battle.EnemyContenders
                     .Where(enemy =>
                         enemy != character
                         && int3.Distance(character.LocationPosition, enemy.LocationPosition) <= distance))
        {
            effectPower.ApplyEffectOnCharacter(enemy.RulesetCharacter, true, enemy.LocationPosition);
        }
    }
}
