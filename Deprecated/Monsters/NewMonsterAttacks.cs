﻿using System;
using System.Collections.Generic;
using SolastaCommunityExpansion.Api;
using SolastaCommunityExpansion.Builders;
using SolastaMonsters.Models;
using UnityEngine;

//******************************************************************************************
// BY DEFINITION, REFACTORING REQUIRES CONFIRMING EXTERNAL BEHAVIOUR DOES NOT CHANGE
// "REFACTORING WITHOUT TESTS IS JUST CHANGING STUFF"
//******************************************************************************************
namespace SolastaMonsters.Monsters;

public class NewMonsterAttacks
{
    public static MonsterAttackDefinition FireScimatar_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition LightningScimatar_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition HurlFlame_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition AirBlast_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition PoisonLongsword_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition PoisonLongbow_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition RadiantLongsword_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition RadiantLongbow_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition NagaBite_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition NagaSpit_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition Ice_Bite_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition Roc_Beak_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition Roc_Talons_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition Generic_Bite_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition PitFiend_Bite_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition PitFiend_Mace_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition Generic_Stronger_Bite_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition AncientDragon_Tail_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition Fork_Attack = ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition HornedDevilTail_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition AncientDragon_Claw_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition Balor_Longsword_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition Balor_Whip_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition Lich_ParalyzingTouch_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition FireTitan_Slam_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition AirTitan_Slam_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition EarthTitan_Slam_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition ConstructTitan_Slam_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition ConstructTitan_ForceCannon_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition EarthTitan_Boulder_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static ConditionDefinition TarrasqueGrappledRestrainedCondition =
        ScriptableObject.CreateInstance<ConditionDefinition>();

    public static MonsterAttackDefinition Tarrasque_Bite_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition Tarrasque_Claw_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition Tarrasque_Tail_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static MonsterAttackDefinition Tarrasque_Horn_Attack =
        ScriptableObject.CreateInstance<MonsterAttackDefinition>();

    public static Dictionary<string, MonsterAttackDefinition> DictionaryOfAncientDragonBites = new();
    public static Dictionary<string, MonsterAttackDefinition> DictionaryOfGenericBitesWithExtraDamage = new();

    internal static void Create()
    {
        BuildNewIce_Bite_Attack();
        BuildNewPoisonLongsword_Attack();
        BuildNewPoisonLongbow_Attack();
        BuildNewRadiantLongsword_Attack();
        BuildNewRadiantLongbow_Attack();
        BuildNewAirBlast_Attack();
        BuildNewHurlFlame_Attack();
        BuildNewFireScimatar_Attack();
        BuildNewLightningScimatar_Attack();
        BuildNewNagaBite_Attack();
        BuildNewNagaSpit_Attack();
        BuildNewRoc_Beak_Attack();
        BuildNewRoc_Talons_Attack();
        BuildNewFork_Attack();
        BuildNewHornedDevilTail_Attack();
        BuildNewGeneric_Bite_Attack();
        BuildNewGeneric_Stronger_Bite_Attack();
        BuildNewGeneric_Claw_Attack();
        BuildNew_AncientDragon_Bite_Attack();
        BuildNewAncientDragon_Tail_Attack();
        BuildNewPitFiend_Bite_Attack();
        BuildNew_PitFiend_Mace_Attack();
        BuildNewBalor_Longsword_Attack();
        BuildNewBalor_Whip_Attack();
        BuildNewLich_ParalyzingTouch_Attack();
        BuildNewAirTitan_Slam_Attack();
        BuildNewFireTitan_Slam_Attack();
        BuildNewEarthTitan_Boulder_Attack();
        BuildNewEarthTitan_Slam_Attack();
        BuildNewConstructTitan_Slam_Attack();
        BuildNewConstructTitan_ForceCannon_Attack();
        BuildNewTarrasque_Bite_Attack();
        BuildNewTarrasque_Claw_Attack();
        BuildNewTarrasque_Tail_Attack();
        BuildNewTarrasque_Horn_Attack();
    }

    public static void BuildNewRoc_Beak_Attack()
    {
        var text = "Roc_Beak_Attack";


        Roc_Beak_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_GiantEagle_Beak,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        Roc_Beak_Attack.toHitBonus = 13;
        Roc_Beak_Attack.reachRange = 2;
        Roc_Beak_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 4;
        Roc_Beak_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D8;
        Roc_Beak_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 9;
        Roc_Beak_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypePiercing;
    }

    public static void BuildNewRoc_Talons_Attack()
    {
        var text = "Roc_Talons_Attack";


        Roc_Talons_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_GiantEagle_Talons,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        Roc_Talons_Attack.toHitBonus = 13;
        Roc_Talons_Attack.reachRange = 1;
        Roc_Talons_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 4;
        Roc_Talons_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;
        Roc_Talons_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 9;
        Roc_Talons_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeSlashing;


        MotionForm motionForm = new();
        motionForm.distance = 10;
        motionForm.type = MotionForm.MotionType.Levitate;

        EffectForm FallingEffect = new();
        FallingEffect.applyLevel = EffectForm.LevelApplianceType.No;
        FallingEffect.levelMultiplier = 1;
        FallingEffect.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        FallingEffect.createdByCondition = true;
        FallingEffect.FormType = EffectForm.EffectFormType.Motion;
        FallingEffect.motionForm = motionForm;
        FallingEffect.hasSavingThrow = false;
        FallingEffect.canSaveToCancel = false;
        FallingEffect.saveOccurence = RuleDefinitions.TurnOccurenceType.StartOfTurn;

        Roc_Talons_Attack.EffectDescription.EffectForms.Add(FallingEffect);

        Roc_Talons_Attack.EffectDescription.fixedSavingThrowDifficultyClass = 19;
        Roc_Talons_Attack.EffectDescription.savingThrowAbility = DatabaseHelper.SmartAttributeDefinitions.Dexterity
            .Name;
    }

    public static void BuildNewIce_Bite_Attack()
    {
        var text = "Ice_Bite_Attack";


        Ice_Bite_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Green_Dragon_Bite,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        Ice_Bite_Attack.reachRange = 1;
        Ice_Bite_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;

        Ice_Bite_Attack.EffectDescription.EffectForms[1].DamageForm.diceNumber = 3;
        Ice_Bite_Attack.EffectDescription.EffectForms[1].DamageForm.damageType = RuleDefinitions.DamageTypeCold;

        Ice_Bite_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper.MonsterAttackDefinitions
            .Attack_Orc_Grimblade_IceDagger.EffectDescription.EffectParticleParameters);
    }

    public static void BuildNewNagaSpit_Attack()
    {
        var text = "NagaSpit_Attack";


        NagaSpit_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Spider_Crimson_Spit,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        NagaSpit_Attack.toHitBonus = 8;
        NagaSpit_Attack.EffectDescription.rangeParameter = 6;
        NagaSpit_Attack.reachRange = 6;
        NagaSpit_Attack.maxRange = 6;
        NagaSpit_Attack.closeRange = 6;
        NagaSpit_Attack.maxUses = -1;
        NagaSpit_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 10;
        NagaSpit_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D8;
        NagaSpit_Attack.EffectDescription.EffectForms[0].DamageForm.damageType = RuleDefinitions.DamageTypePoison;
        NagaSpit_Attack.EffectDescription.EffectForms[0].hasSavingThrow = true;
        NagaSpit_Attack.EffectDescription.EffectForms[0]
            .savingThrowAffinity = RuleDefinitions.EffectSavingThrowType.HalfDamage;
        NagaSpit_Attack.EffectDescription.savingThrowAbility = DatabaseHelper.SmartAttributeDefinitions
            .Constitution.Name;
        NagaSpit_Attack.EffectDescription.savingThrowDifficultyAbility = DatabaseHelper.SmartAttributeDefinitions
            .Constitution.Name;
        NagaSpit_Attack.EffectDescription.hasSavingThrow = true;
        NagaSpit_Attack.EffectDescription.fixedSavingThrowDifficultyClass = 15;
    }

    public static void BuildNewNagaBite_Attack()
    {
        var text = "NagaBite_Attack";


        NagaBite_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Goblin_PebbleThrow,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        NagaBite_Attack.toHitBonus = 7;
        NagaBite_Attack.proximity = RuleDefinitions.AttackProximity.Melee;
        NagaBite_Attack.EffectDescription.rangeParameter = 2;
        NagaBite_Attack.reachRange = 2;
        NagaBite_Attack.maxRange = 2;
        NagaBite_Attack.closeRange = 2;
        NagaBite_Attack.maxUses = -1;
        NagaBite_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 1;
        NagaBite_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D8;
        NagaBite_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 4;
        NagaBite_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypePiercing;

        DamageForm damageForm = new();
        damageForm.diceNumber = 10;
        damageForm.dieType = RuleDefinitions.DieType.D8;
        damageForm.bonusDamage = 0;
        damageForm.damageType = RuleDefinitions.DamageTypePoison;


        EffectForm extraDamageEffect = new();
        extraDamageEffect.applyLevel = EffectForm.LevelApplianceType.No;
        extraDamageEffect.levelMultiplier = 1;
        extraDamageEffect.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        extraDamageEffect.createdByCharacter = true;
        extraDamageEffect.FormType = EffectForm.EffectFormType.Damage;
        extraDamageEffect.damageForm = damageForm;
        extraDamageEffect.hasSavingThrow = true;
        extraDamageEffect.savingThrowAffinity = RuleDefinitions.EffectSavingThrowType.HalfDamage;

        NagaBite_Attack.EffectDescription.EffectForms.Add(extraDamageEffect);
        NagaBite_Attack.EffectDescription.savingThrowAbility = DatabaseHelper.SmartAttributeDefinitions
            .Constitution.Name;
        NagaBite_Attack.EffectDescription.savingThrowDifficultyAbility = DatabaseHelper.SmartAttributeDefinitions
            .Constitution.Name;
        NagaBite_Attack.EffectDescription.hasSavingThrow = true;
        NagaBite_Attack.EffectDescription.fixedSavingThrowDifficultyClass = 15;
    }

    public static void BuildNewFork_Attack()
    {
        var text = "Fork_Attack";


        Fork_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Skeleton_Spear,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        Fork_Attack.toHitBonus = 10;
        Fork_Attack.reachRange = 2;
        Fork_Attack.maxRange = 3;
        Fork_Attack.closeRange = 2;
        Fork_Attack.proximity = RuleDefinitions.AttackProximity.Melee;
        Fork_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 2;
        Fork_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D8;
        Fork_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 6;
        Fork_Attack.EffectDescription.EffectForms[0].DamageForm.damageType = RuleDefinitions.DamageTypePiercing;
    }

    public static void BuildNewHornedDevilTail_Attack()
    {
        var text = "HornedDevilTail_Attack";

        var BleedingWound_Condition = BuildNewCondition(
            "DH_Custom_" + text + "condition",
            DatabaseHelper.ConditionDefinitions.ConditionBleeding,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text + "condition").ToString(),
            "MonsterAttack/&DH_" + text + "condition" + "_Title",
            "MonsterAttack/&DH_" + text + "condition" + "_Description"
        );

        BleedingWound_Condition.allowMultipleInstances = true;
        BleedingWound_Condition.RecurrentEffectForms[0].DamageForm.diceNumber = 3;
        BleedingWound_Condition.RecurrentEffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;


        HornedDevilTail_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Green_Dragon_Tail,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );


        ConditionForm TailCondition = new();
        TailCondition.applyToSelf = false;
        TailCondition.forceOnSelf = false;
        TailCondition.Operation = ConditionForm.ConditionOperation.Add;
        TailCondition.conditionDefinitionName = BleedingWound_Condition.Name;
        TailCondition.ConditionDefinition = BleedingWound_Condition;

        EffectForm TailEffect = new();
        TailEffect.applyLevel = EffectForm.LevelApplianceType.No;
        TailEffect.levelMultiplier = 1;
        TailEffect.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        TailEffect.createdByCharacter = true;
        TailEffect.FormType = EffectForm.EffectFormType.Condition;
        TailEffect.ConditionForm = TailCondition;
        TailEffect.hasSavingThrow = true;
        TailEffect.canSaveToCancel = true;
        TailEffect.savingThrowAffinity = RuleDefinitions.EffectSavingThrowType.Negates;


        HornedDevilTail_Attack.toHitBonus = 10;
        HornedDevilTail_Attack.reachRange = 2;
        HornedDevilTail_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 2;
        HornedDevilTail_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D8;
        HornedDevilTail_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 6;
        HornedDevilTail_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypePiercing;

        HornedDevilTail_Attack.EffectDescription.EffectForms.Add(TailEffect);
        HornedDevilTail_Attack.EffectDescription.savingThrowAbility = DatabaseHelper.SmartAttributeDefinitions
            .Wisdom.name;
        HornedDevilTail_Attack.EffectDescription.difficultyClassComputation =
            RuleDefinitions.EffectDifficultyClassComputation.FixedValue;
        HornedDevilTail_Attack.EffectDescription.fixedSavingThrowDifficultyClass = 12;
    }

    public static void BuildNewPoisonLongsword_Attack()
    {
        var text = "PoisonLongsword_Attack";


        PoisonLongsword_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Veteran_Sorak_Agent_Longsword,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        PoisonLongsword_Attack.toHitBonus = 8;
        PoisonLongsword_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 1;
        PoisonLongsword_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D10;
        PoisonLongsword_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 4;
        PoisonLongsword_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeSlashing;

        DamageForm damageForm = new();
        damageForm.diceNumber = 3;
        damageForm.dieType = RuleDefinitions.DieType.D8;
        damageForm.bonusDamage = 0;
        damageForm.damageType = RuleDefinitions.DamageTypePoison;

        EffectForm extraDamageEffect = new();
        extraDamageEffect.applyLevel = EffectForm.LevelApplianceType.No;
        extraDamageEffect.levelMultiplier = 1;
        extraDamageEffect.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        extraDamageEffect.createdByCharacter = true;
        extraDamageEffect.FormType = EffectForm.EffectFormType.Damage;
        extraDamageEffect.damageForm = damageForm;

        PoisonLongsword_Attack.EffectDescription.EffectForms.Add(extraDamageEffect);
        PoisonLongsword_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper.MonsterAttackDefinitions
            .Attack_PoisonousSnake_Bite.EffectDescription.EffectParticleParameters);
    }

    public static void BuildNewPoisonLongbow_Attack()
    {
        var text = "PoisonLongbow_Attack";


        PoisonLongbow_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_BadlandHunter_Longbow,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        PoisonLongbow_Attack.toHitBonus = 7;
        PoisonLongbow_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 1;
        PoisonLongbow_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D8;
        PoisonLongbow_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 3;
        PoisonLongbow_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypePiercing;

        DamageForm damageForm = new();
        damageForm.diceNumber = 3;
        damageForm.dieType = RuleDefinitions.DieType.D8;
        damageForm.bonusDamage = 0;
        damageForm.damageType = RuleDefinitions.DamageTypePoison;

        EffectForm extraDamageEffect = new();
        extraDamageEffect.applyLevel = EffectForm.LevelApplianceType.No;
        extraDamageEffect.levelMultiplier = 1;
        extraDamageEffect.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        extraDamageEffect.createdByCharacter = true;
        extraDamageEffect.FormType = EffectForm.EffectFormType.Damage;
        extraDamageEffect.damageForm = damageForm;

        ConditionForm PoisonLongbowCondition = new();
        PoisonLongbowCondition.applyToSelf = false;
        PoisonLongbowCondition.forceOnSelf = false;
        PoisonLongbowCondition.Operation = ConditionForm.ConditionOperation.Add;
        PoisonLongbowCondition.conditionDefinitionName = DatabaseHelper.ConditionDefinitions.ConditionPoisoned.Name;
        PoisonLongbowCondition.ConditionDefinition = DatabaseHelper.ConditionDefinitions.ConditionPoisoned;

        EffectForm PoisonLongbowEffect = new();
        PoisonLongbowEffect.applyLevel = EffectForm.LevelApplianceType.No;
        PoisonLongbowEffect.levelMultiplier = 1;
        PoisonLongbowEffect.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        PoisonLongbowEffect.createdByCharacter = true;
        PoisonLongbowEffect.FormType = EffectForm.EffectFormType.Condition;
        PoisonLongbowEffect.ConditionForm = PoisonLongbowCondition;
        PoisonLongbowEffect.hasSavingThrow = true;
        PoisonLongbowEffect.canSaveToCancel = false;
        PoisonLongbowEffect.savingThrowAffinity = RuleDefinitions.EffectSavingThrowType.Negates;


        PoisonLongbow_Attack.EffectDescription.EffectForms.Add(extraDamageEffect);
        PoisonLongbow_Attack.EffectDescription.EffectForms.Add(PoisonLongbowEffect);

        PoisonLongbow_Attack.EffectDescription.savingThrowAbility = DatabaseHelper.SmartAttributeDefinitions
            .Constitution.name;
        PoisonLongbow_Attack.EffectDescription.difficultyClassComputation =
            RuleDefinitions.EffectDifficultyClassComputation.FixedValue;
        PoisonLongbow_Attack.EffectDescription.fixedSavingThrowDifficultyClass = 14;
        PoisonLongbow_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper.MonsterAttackDefinitions
            .Attack_PoisonousSnake_Bite.EffectDescription.EffectParticleParameters);
    }

    public static void BuildNewRadiantLongsword_Attack()
    {
        var text = "RadiantLongsword_Attack";


        RadiantLongsword_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Hyeronimus_Greatsword,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        RadiantLongsword_Attack.toHitBonus = 15;
        RadiantLongsword_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 4;
        RadiantLongsword_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;
        RadiantLongsword_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 8;
        RadiantLongsword_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeSlashing;

        RadiantLongsword_Attack.EffectDescription.EffectForms[1].DamageForm.diceNumber = 6;
        RadiantLongsword_Attack.EffectDescription.EffectForms[1].DamageForm.dieType = RuleDefinitions.DieType.D8;

        RadiantLongsword_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper.MonsterAttackDefinitions
            .Attack_Divine_Avatar.EffectDescription.EffectParticleParameters);
    }

    public static void BuildNewRadiantLongbow_Attack()
    {
        var text = "RadiantLongbow_Attack";


        RadiantLongbow_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_BadlandHunter_Longbow,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        RadiantLongbow_Attack.toHitBonus = 13;
        RadiantLongbow_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 2;
        RadiantLongbow_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D8;
        RadiantLongbow_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 8;
        RadiantLongbow_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypePiercing;

        DamageForm damageForm = new();
        damageForm.diceNumber = 6;
        damageForm.dieType = RuleDefinitions.DieType.D8;
        damageForm.bonusDamage = 0;
        damageForm.damageType = RuleDefinitions.DamageTypeRadiant;

        EffectForm extraDamageEffect = new();
        extraDamageEffect.applyLevel = EffectForm.LevelApplianceType.No;
        extraDamageEffect.levelMultiplier = 1;
        extraDamageEffect.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        extraDamageEffect.createdByCharacter = true;
        extraDamageEffect.FormType = EffectForm.EffectFormType.Damage;
        extraDamageEffect.damageForm = damageForm;

        KillForm killForm = new();
        killForm.killCondition = RuleDefinitions.KillCondition.UnderHitPoints;
        killForm.hitPoints = 100;

        EffectForm killEffect = new();
        killEffect.applyLevel = EffectForm.LevelApplianceType.No;
        killEffect.levelMultiplier = 1;
        killEffect.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        killEffect.createdByCharacter = true;
        killEffect.formType = EffectForm.EffectFormType.Kill;
        killEffect.killForm = killForm;
        killEffect.hasSavingThrow = true;
        killEffect.savingThrowAffinity = RuleDefinitions.EffectSavingThrowType.Negates;

        RadiantLongbow_Attack.EffectDescription.EffectForms.Add(extraDamageEffect);

        RadiantLongbow_Attack.EffectDescription.EffectForms.Add(killEffect);

        RadiantLongbow_Attack.EffectDescription.savingThrowAbility = DatabaseHelper.SmartAttributeDefinitions
            .Constitution.name;
        RadiantLongbow_Attack.EffectDescription.difficultyClassComputation =
            RuleDefinitions.EffectDifficultyClassComputation.FixedValue;
        RadiantLongbow_Attack.EffectDescription.fixedSavingThrowDifficultyClass = 15;
        RadiantLongbow_Attack.EffectDescription.hasSavingThrow = true;
        RadiantLongbow_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper.MonsterAttackDefinitions
            .Attack_Divine_Avatar.EffectDescription.EffectParticleParameters);
    }

    public static void BuildNewAirBlast_Attack()
    {
        var text = "AirBlast_Attack";


        AirBlast_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Goblin_PebbleThrow,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        AirBlast_Attack.toHitBonus = 7;
        AirBlast_Attack.EffectDescription.rangeParameter = 24;
        AirBlast_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 5;
        AirBlast_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;
        AirBlast_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeThunder;

        MotionForm motion = new();
        motion.distance = 2;
        motion.type = MotionForm.MotionType.PushFromOrigin;

        EffectForm motionEffect = new() {FormType = EffectForm.EffectFormType.Motion};
        motionEffect.motionForm = motion;
        motionEffect.applyLevel = EffectForm.LevelApplianceType.No;
        motionEffect.levelType = RuleDefinitions.LevelSourceType.CharacterLevel;
        motionEffect.levelMultiplier = 1;

        AirBlast_Attack.EffectDescription.EffectForms.Add(motionEffect);
    }

    public static void BuildNewHurlFlame_Attack()
    {
        var text = "HurlFlame_Attack";


        HurlFlame_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Fire_Jester_Firebolt,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        HurlFlame_Attack.toHitBonus = 7;
        HurlFlame_Attack.EffectDescription.rangeParameter = 24;
        HurlFlame_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 5;
        HurlFlame_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;
        HurlFlame_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper.MonsterAttackDefinitions
            .Attack_FireOsprey_Touch.EffectDescription.EffectParticleParameters);
    }

    public static void BuildNewFireScimatar_Attack()
    {
        var text = "FireScimatar_Attack";


        FireScimatar_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Goblin_Cutthroat_Scimitar,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        FireScimatar_Attack.toHitBonus = 10;
        FireScimatar_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 2;
        FireScimatar_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 6;
        FireScimatar_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;

        DamageForm damageForm = new();
        damageForm.diceNumber = 2;
        damageForm.dieType = RuleDefinitions.DieType.D6;
        damageForm.bonusDamage = 0;
        damageForm.damageType = RuleDefinitions.DamageTypeFire;

        EffectForm extraFireDamageEffect = new();
        extraFireDamageEffect.applyLevel = EffectForm.LevelApplianceType.No;
        extraFireDamageEffect.levelMultiplier = 1;
        extraFireDamageEffect.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        extraFireDamageEffect.createdByCharacter = true;
        extraFireDamageEffect.FormType = EffectForm.EffectFormType.Damage;
        extraFireDamageEffect.damageForm = damageForm;

        FireScimatar_Attack.EffectDescription.EffectForms.Add(extraFireDamageEffect);
        FireScimatar_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper.MonsterAttackDefinitions
            .Attack_FireOsprey_Touch.EffectDescription.EffectParticleParameters);
    }

    public static void BuildNewLightningScimatar_Attack()
    {
        var text = "LightningScimatar_Attack";


        LightningScimatar_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Goblin_Cutthroat_Scimitar,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        LightningScimatar_Attack.toHitBonus = 10;
        LightningScimatar_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 2;
        LightningScimatar_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 6;
        LightningScimatar_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;

        DamageForm damageForm = new();
        damageForm.diceNumber = 2;
        damageForm.dieType = RuleDefinitions.DieType.D6;
        damageForm.bonusDamage = 0;
        damageForm.damageType = RuleDefinitions.DamageTypeLightning;

        EffectForm extraFireDamageEffect = new();
        extraFireDamageEffect.applyLevel = EffectForm.LevelApplianceType.No;
        extraFireDamageEffect.levelMultiplier = 1;
        extraFireDamageEffect.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        extraFireDamageEffect.createdByCharacter = true;
        extraFireDamageEffect.FormType = EffectForm.EffectFormType.Damage;
        extraFireDamageEffect.damageForm = damageForm;

        LightningScimatar_Attack.EffectDescription.EffectForms.Add(extraFireDamageEffect);
        LightningScimatar_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper.MonsterAttackDefinitions
            .Attack_ZealotShockingAntenna.EffectDescription.EffectParticleParameters);
    }

    public static void BuildNewGeneric_Bite_Attack()
    {
        // generic bite attack without extra damage for CR 10-15 monsters
        var text = "Generic_Bite_Attack_No_ExtraDamage";


        Generic_Bite_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Green_Dragon_Bite,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        Generic_Bite_Attack.reachRange = 1;
        Generic_Bite_Attack.EffectDescription.EffectForms.RemoveAt(1);
        Generic_Bite_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper.MonsterAttackDefinitions
            .Attack_BrownBear_Bite.EffectDescription.EffectParticleParameters);
    }

    public static void BuildNewGeneric_Stronger_Bite_Attack()
    {
        // generic bite attack without extra damage for high level CR monsters
        var text_1 = "Generic_Stronger_Bite_Attack_No_ExtraDamage";


        Generic_Stronger_Bite_Attack = BuildNewAttack(
            "DH_Custom_" + text_1,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Green_Dragon_Bite,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text_1).ToString(),
            "MonsterAttack/&DH_" + text_1 + "_Title",
            "MonsterAttack/&DH_" + text_1 + "_Description"
        );


        Generic_Stronger_Bite_Attack.toHitBonus = 15;
        Generic_Stronger_Bite_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 5;
        Generic_Stronger_Bite_Attack.EffectDescription.EffectForms[0].DamageForm
            .dieType = RuleDefinitions.DieType.D10;
        Generic_Stronger_Bite_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 5;
        Generic_Stronger_Bite_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper
            .MonsterAttackDefinitions.Attack_BrownBear_Bite.EffectDescription.EffectParticleParameters);

        Generic_Stronger_Bite_Attack.EffectDescription.EffectForms.RemoveAt(1);
    }


    public static void BuildNewGeneric_Claw_Attack()
    {
        // correct dice numbers/type for ancient dragon claw
        var text = "Generic_Claw_Attack";


        AncientDragon_Claw_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Green_Dragon_Claw,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        // generic ancient dragon Claw attack
        AncientDragon_Claw_Attack.toHitBonus = 15;
        AncientDragon_Claw_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 3;
        AncientDragon_Claw_Attack.EffectDescription.EffectForms[0].DamageForm
            .dieType = RuleDefinitions.DieType.D6;
        AncientDragon_Claw_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 9;
    }


    public static void BuildNew_AncientDragon_Bite_Attack()
    {
        Dictionary<string, int> dictionaryofAncientDragonBiteExtraDamageDiceNumbers = new()
        {
            {"Ancient Black Dragon", 2},
            {"Ancient Blue Dragon", 2},
            {"Ancient Green Dragon", 3},
            {"Ancient Red Dragon", 4},
            {"Ancient White Dragon", 2}
        };

        Dictionary<string, RuleDefinitions.DieType> dictionaryofAncientDragonBiteExtraDamageDiceType = new()
        {
            {"Ancient Black Dragon", RuleDefinitions.DieType.D8},
            {"Ancient Blue Dragon", RuleDefinitions.DieType.D10},
            {"Ancient Green Dragon", RuleDefinitions.DieType.D6},
            {"Ancient Red Dragon", RuleDefinitions.DieType.D6},
            {"Ancient White Dragon", RuleDefinitions.DieType.D8}
        };

        Dictionary<string, EffectParticleParameters> dictionaryofAncientDragonBiteEffectparticles = new()
        {
            {
                "Ancient Black Dragon", DatabaseHelper.MonsterAttackDefinitions.Attack_Black_Dragon_Bite
                    .EffectDescription
                    .EffectParticleParameters
            },
            {
                "Ancient Blue Dragon", DatabaseHelper.MonsterAttackDefinitions.Attack_ZealotShockingAntenna
                    .EffectDescription
                    .EffectParticleParameters
            },
            {
                "Ancient Green Dragon", DatabaseHelper.MonsterAttackDefinitions.Attack_Green_Dragon_Bite
                    .EffectDescription
                    .EffectParticleParameters
            },
            {
                "Ancient Red Dragon", DatabaseHelper.MonsterAttackDefinitions.Attack_Fire_Elemental_Touch
                    .EffectDescription
                    .EffectParticleParameters
            },
            {
                "Ancient White Dragon", DatabaseHelper.MonsterAttackDefinitions.Attack_Orc_Grimblade_IceDagger
                    .EffectDescription
                    .EffectParticleParameters
            }
        };


        foreach (var entry in NewMonsterAttributes.Dictionaryof_Dragon_DamageAffinity)
        {
            var text = entry.Value + "_Bite_Attack";
            text = text.Replace(" ", "");

            var Dragon_Bite_Attack = BuildNewAttack(
                "DH_Custom_" + text,
                DatabaseHelper.MonsterAttackDefinitions.Attack_Green_Dragon_Bite,
                GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
                "MonsterAttack/&DH_" + text + "_Title",
                "MonsterAttack/&DH_" + text + "_Description"
            );

            Dragon_Bite_Attack.reachRange = 3;
            Dragon_Bite_Attack.toHitBonus = 15;
            Dragon_Bite_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 2;
            Dragon_Bite_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D10;
            Dragon_Bite_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 9;

            // extra damage
            Dragon_Bite_Attack.EffectDescription.EffectForms[1].DamageForm
                .diceNumber = dictionaryofAncientDragonBiteExtraDamageDiceNumbers[entry.Key];
            Dragon_Bite_Attack.EffectDescription.EffectForms[1].DamageForm
                .dieType = dictionaryofAncientDragonBiteExtraDamageDiceType[entry.Key];
            Dragon_Bite_Attack.EffectDescription.EffectForms[1].DamageForm
                .damageType = entry.Value; // ListofDamageTypes_Dragon[i]);
            Dragon_Bite_Attack.EffectDescription.effectParticleParameters =
                dictionaryofAncientDragonBiteEffectparticles[entry.Key];


            DictionaryOfAncientDragonBites.Add(entry.Key, Dragon_Bite_Attack);


            DictionaryOfGenericBitesWithExtraDamage.Add(entry.Value, Dragon_Bite_Attack);
        }
    }


    public static void BuildNewAncientDragon_Tail_Attack()
    {
        // correct dice numbers/type for ancient dragon tail
        var text = "AncientDragon_Tail_Attack";


        AncientDragon_Tail_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Green_Dragon_Tail,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        // generic ancient dragon Tail attack
        AncientDragon_Tail_Attack.reachRange = 4;
        AncientDragon_Tail_Attack.toHitBonus = 15;
        AncientDragon_Tail_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 2;
        AncientDragon_Tail_Attack.EffectDescription.EffectForms[0].DamageForm
            .dieType = RuleDefinitions.DieType.D8;
        AncientDragon_Tail_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 8;
    }


    public static void BuildNewPitFiend_Bite_Attack()
    {
        var text = "PitFiend_Bite_Attack";

        var PitFiend_Bite_Condition = BuildNewCondition(
            "DH_Custom_" + text + "condition",
            DatabaseHelper.ConditionDefinitions.ConditionPoisoned_BasicPoison,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text + "condition").ToString(),
            "MonsterAttack/&DH_" + text + "condition" + "_Title",
            "MonsterAttack/&DH_" + text + "condition" + "_Description"
        );

        PitFiend_Bite_Condition.RecurrentEffectForms[0].DamageForm.diceNumber = 6;
        PitFiend_Bite_Condition.RecurrentEffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;


        PitFiend_Bite_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Green_Dragon_Bite,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        ConditionForm PitFiendBiteCondition = new();
        PitFiendBiteCondition.applyToSelf = false;
        PitFiendBiteCondition.forceOnSelf = false;
        PitFiendBiteCondition.Operation = ConditionForm.ConditionOperation.Add;
        PitFiendBiteCondition.conditionDefinitionName = PitFiend_Bite_Condition.Name;
        PitFiendBiteCondition.ConditionDefinition = PitFiend_Bite_Condition;

        EffectForm PitFiendBiteEffect = new();
        PitFiendBiteEffect.applyLevel = EffectForm.LevelApplianceType.No;
        PitFiendBiteEffect.levelMultiplier = 1;
        PitFiendBiteEffect.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        PitFiendBiteEffect.createdByCharacter = true;
        PitFiendBiteEffect.FormType = EffectForm.EffectFormType.Condition;
        PitFiendBiteEffect.ConditionForm = PitFiendBiteCondition;
        PitFiendBiteEffect.hasSavingThrow = true;
        PitFiendBiteEffect.canSaveToCancel = true;
        PitFiendBiteEffect.savingThrowAffinity = RuleDefinitions.EffectSavingThrowType.Negates;

        PitFiend_Bite_Attack.reachRange = 2;
        PitFiend_Bite_Attack.EffectDescription.EffectForms.Add(PitFiendBiteEffect);
        PitFiend_Bite_Attack.EffectDescription.savingThrowAbility = DatabaseHelper.SmartAttributeDefinitions
            .Constitution.name;
        PitFiend_Bite_Attack.EffectDescription.difficultyClassComputation =
            RuleDefinitions.EffectDifficultyClassComputation.FixedValue;
        PitFiend_Bite_Attack.EffectDescription.fixedSavingThrowDifficultyClass = 21;
    }


    public static void BuildNew_PitFiend_Mace_Attack()
    {
        var text = "PitFiend_Mace_Attack";


        PitFiend_Mace_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Divine_Avatar,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );


        PitFiend_Mace_Attack.reachRange = 2;
        PitFiend_Mace_Attack.toHitBonus = 14;
        PitFiend_Mace_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 2;
        PitFiend_Mace_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;
        PitFiend_Mace_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 8;

        // extra damage
        PitFiend_Mace_Attack.EffectDescription.EffectForms[1].DamageForm.diceNumber = 6;
        PitFiend_Mace_Attack.EffectDescription.EffectForms[1].DamageForm.dieType = RuleDefinitions.DieType.D6;
        PitFiend_Mace_Attack.EffectDescription.EffectForms[1].DamageForm
            .damageType = "DamageFire"; // ListofDamageTypes_Dragon[i]);
    }

    public static void BuildNewBalor_Longsword_Attack()
    {
        var text = "Balor_Longsword_Attack";


        Balor_Longsword_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Divine_Avatar,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        Balor_Longsword_Attack.reachRange = 2;
        Balor_Longsword_Attack.toHitBonus = 14;
        Balor_Longsword_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 3;
        Balor_Longsword_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D8;
        Balor_Longsword_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 8;
        Balor_Longsword_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeSlashing;


        Balor_Longsword_Attack.EffectDescription.EffectForms[1].DamageForm.diceNumber = 3;
        Balor_Longsword_Attack.EffectDescription.EffectForms[1].DamageForm.dieType = RuleDefinitions.DieType.D8;
        Balor_Longsword_Attack.EffectDescription.EffectForms[1].DamageForm.bonusDamage = 0;
        Balor_Longsword_Attack.EffectDescription.EffectForms[1].DamageForm
            .damageType = RuleDefinitions.DamageTypeLightning;

        Balor_Longsword_Attack.itemDefinitionMainHand = DatabaseHelper.ItemDefinitions
            .Enchanted_Greataxe_Stormblade;
    }

    public static void BuildNewBalor_Whip_Attack()
    {
        var text = "Balor_Whip_Attack";


        Balor_Whip_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Divine_Avatar,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        Balor_Whip_Attack.reachRange = 6;
        Balor_Whip_Attack.maxRange = 6;
        Balor_Whip_Attack.toHitBonus = 14;
        Balor_Whip_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 2;
        Balor_Whip_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;
        Balor_Whip_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 8;
        Balor_Whip_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeSlashing;

        Balor_Whip_Attack.EffectDescription.EffectForms[1].DamageForm.diceNumber = 3;
        Balor_Whip_Attack.EffectDescription.EffectForms[1].DamageForm.dieType = RuleDefinitions.DieType.D6;
        Balor_Whip_Attack.EffectDescription.EffectForms[1].DamageForm.bonusDamage = 0;
        Balor_Whip_Attack.EffectDescription.EffectForms[1].DamageForm.damageType = RuleDefinitions.DamageTypeFire;

        MotionForm motionForm = new();
        motionForm.distance = 5;
        motionForm.type = MotionForm.MotionType.DragToOrigin;

        EffectForm effectForm = new();
        effectForm.applyLevel = EffectForm.LevelApplianceType.No;
        effectForm.levelMultiplier = 1;
        effectForm.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        effectForm.createdByCharacter = true;
        effectForm.FormType = EffectForm.EffectFormType.Motion;
        effectForm.motionForm = motionForm;
        effectForm.hasSavingThrow = true;
        effectForm.savingThrowAffinity = RuleDefinitions.EffectSavingThrowType.Negates;


        Balor_Whip_Attack.EffectDescription.EffectForms.Add(effectForm);
        Balor_Whip_Attack.EffectDescription.savingThrowAbility = DatabaseHelper.SmartAttributeDefinitions.Strength
            .Name;
        Balor_Whip_Attack.EffectDescription.hasSavingThrow = true;
        Balor_Whip_Attack.EffectDescription.difficultyClassComputation =
            RuleDefinitions.EffectDifficultyClassComputation.FixedValue;
        Balor_Whip_Attack.EffectDescription.fixedSavingThrowDifficultyClass = 20;
    }


    public static void BuildNewLich_ParalyzingTouch_Attack()
    {
        var text = "Lich_ParalyzingTouch_Attack";


        Lich_ParalyzingTouch_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Ghost_Withering_Laethar,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        Lich_ParalyzingTouch_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 3;
        Lich_ParalyzingTouch_Attack.EffectDescription.EffectForms[0].DamageForm
            .dieType = RuleDefinitions.DieType.D6;
        Lich_ParalyzingTouch_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 0;
        Lich_ParalyzingTouch_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeCold;


        Lich_ParalyzingTouch_Attack.toHitBonus = 12;
        Lich_ParalyzingTouch_Attack.EffectDescription.EffectForms[1].ConditionForm
            .conditionDefinition = DatabaseHelper.ConditionDefinitions.ConditionParalyzed;
        Lich_ParalyzingTouch_Attack.EffectDescription.savingThrowAbility = DatabaseHelper.SmartAttributeDefinitions
            .Constitution.name;
        Lich_ParalyzingTouch_Attack.EffectDescription.difficultyClassComputation =
            RuleDefinitions.EffectDifficultyClassComputation.FixedValue;
        Lich_ParalyzingTouch_Attack.EffectDescription.fixedSavingThrowDifficultyClass = 21;
    }


    public static void BuildNewFireTitan_Slam_Attack()
    {
        var text = "FireTitan_Slam_Attack";


        FireTitan_Slam_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Air_Elemental_Slam,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        FireTitan_Slam_Attack.toHitBonus = 12;
        FireTitan_Slam_Attack.reachRange = 3;
        FireTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 3;
        FireTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;
        FireTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 5;
        FireTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeBludgeoning;

        DamageForm damageForm = new();
        damageForm.diceNumber = 10;
        damageForm.dieType = RuleDefinitions.DieType.D6;
        damageForm.bonusDamage = 0;
        damageForm.damageType = RuleDefinitions.DamageTypeFire;

        EffectForm extraFireDamageEffect = new();
        extraFireDamageEffect.applyLevel = EffectForm.LevelApplianceType.No;
        extraFireDamageEffect.levelMultiplier = 1;
        extraFireDamageEffect.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        extraFireDamageEffect.createdByCharacter = true;
        extraFireDamageEffect.FormType = EffectForm.EffectFormType.Damage;
        extraFireDamageEffect.damageForm = damageForm;

        FireTitan_Slam_Attack.EffectDescription.EffectForms.Add(extraFireDamageEffect);
    }


    public static void BuildNewAirTitan_Slam_Attack()
    {
        var text = "AirTitan_Slam_Attack";


        AirTitan_Slam_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Air_Elemental_Slam,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        AirTitan_Slam_Attack.toHitBonus = 16;
        AirTitan_Slam_Attack.reachRange = 4;
        AirTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 4;
        AirTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;
        AirTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 6;
        AirTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeBludgeoning;

        DamageForm damageForm = new();
        damageForm.diceNumber = 4;
        damageForm.dieType = RuleDefinitions.DieType.D6;
        damageForm.bonusDamage = 0;
        damageForm.damageType = RuleDefinitions.DamageTypeThunder;

        EffectForm extraDamageEffect = new();
        extraDamageEffect.applyLevel = EffectForm.LevelApplianceType.No;
        extraDamageEffect.levelMultiplier = 1;
        extraDamageEffect.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        extraDamageEffect.createdByCharacter = true;
        extraDamageEffect.FormType = EffectForm.EffectFormType.Damage;
        extraDamageEffect.damageForm = damageForm;

        AirTitan_Slam_Attack.EffectDescription.EffectForms.Add(extraDamageEffect);
    }


    public static void BuildNewEarthTitan_Slam_Attack()
    {
        var text = "EarthTitan_Slam_Attack";


        EarthTitan_Slam_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Air_Elemental_Slam,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        EarthTitan_Slam_Attack.toHitBonus = 16;
        EarthTitan_Slam_Attack.reachRange = 4;
        EarthTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 4;
        EarthTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D10;
        EarthTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 8;
        EarthTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeBludgeoning;
    }

    public static void BuildNewConstructTitan_Slam_Attack()
    {
        var text = "ConstructTitan_Slam_Attack";


        ConstructTitan_Slam_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Air_Elemental_Slam,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        ConstructTitan_Slam_Attack.toHitBonus = 18;
        ConstructTitan_Slam_Attack.reachRange = 4;
        ConstructTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 3;
        ConstructTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm
            .dieType = RuleDefinitions.DieType.D12;
        ConstructTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 8;
        ConstructTitan_Slam_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeBludgeoning;

        MotionForm motionForm = new();
        motionForm.type = MotionForm.MotionType.PushFromOrigin;
        motionForm.distance = 4;

        EffectForm effectForm = new();
        effectForm.applyLevel = EffectForm.LevelApplianceType.No;
        effectForm.levelMultiplier = 1;
        effectForm.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        effectForm.createdByCharacter = true;
        effectForm.FormType = EffectForm.EffectFormType.Motion;
        effectForm.motionForm = motionForm;

        ConstructTitan_Slam_Attack.EffectDescription.EffectForms.Add(effectForm);
    }

    public static void BuildNewConstructTitan_ForceCannon_Attack()
    {
        var text = "ConstructTitan_ForceCannon_Attack";


        ConstructTitan_ForceCannon_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Goblin_PebbleThrow,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        ConstructTitan_ForceCannon_Attack.toHitBonus = 18;
        ConstructTitan_ForceCannon_Attack.reachRange = 60;
        ConstructTitan_ForceCannon_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 4;
        ConstructTitan_ForceCannon_Attack.EffectDescription.EffectForms[0].DamageForm
            .dieType = RuleDefinitions.DieType.D8;

        ConstructTitan_ForceCannon_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeForce;
        /*
                    ConstructTitan_ForceCannon_Attack.EffectDescription.effectParticleParameters = (DatabaseHelper.SpellDefinitions.MagicMissile.EffectDescription.EffectParticleParameters);

                    MotionForm motionForm = new MotionForm();
                    motionForm.SetType(MotionForm.MotionType.FallProne);
                    motionForm.SetDistance(6);

                    EffectForm effectForm = new EffectForm();
                    effectForm.applyLevel =(EffectForm.LevelApplianceType.No);
                    effectForm.levelMultiplier =(1);
                    effectForm.levelType =(RuleDefinitions.LevelSourceType.ClassLevel);
                    effectForm.createdByCharacter =(true);
                    effectForm.FormType = EffectForm.EffectFormType.Motion;
                    effectForm.motionForm =(motionForm);

                    ConstructTitan_ForceCannon_Attack.EffectDescription.EffectForms.Add(effectForm);
        */
    }


    public static void BuildNewEarthTitan_Boulder_Attack()
    {
        var text = "EarthTitan_Boulder_Attack";


        EarthTitan_Boulder_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Giant_Fire_Rock,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        EarthTitan_Boulder_Attack.toHitBonus = 6;
        EarthTitan_Boulder_Attack.reachRange = 50;
        EarthTitan_Boulder_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 7;
        EarthTitan_Boulder_Attack.EffectDescription.EffectForms[0].DamageForm
            .dieType = RuleDefinitions.DieType.D10;
        EarthTitan_Boulder_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 8;
        EarthTitan_Boulder_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeBludgeoning;

        EarthTitan_Boulder_Attack.itemDefinitionMainHand = null;

        MotionForm motionForm = new();
        motionForm.type = MotionForm.MotionType.FallProne;
        motionForm.distance = 6;

        EffectForm effectForm = new();
        effectForm.applyLevel = EffectForm.LevelApplianceType.No;
        effectForm.levelMultiplier = 1;
        effectForm.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        effectForm.createdByCharacter = true;
        effectForm.FormType = EffectForm.EffectFormType.Motion;
        effectForm.motionForm = motionForm;

        EarthTitan_Boulder_Attack.EffectDescription.EffectForms.Add(effectForm);
    }

    public static void BuildNewTarrasque_Bite_Attack()
    {
        /*Bite.
        Melee Weapon Attack:                                               Attack_Remorhaz_Bite
        +19 to hit,
        reach 10 ft.,
        one target.
        Hit: 36 (4d12 + 10) piercing damage. If the target is a creature, it is grappled (escape DC 20). Until this grapple ends, the target is restrained, and the tarrasque can't bite another target.
        */

        var text = "Tarrasque_Bite";


        TarrasqueGrappledRestrainedCondition = BuildNewCondition(
            "DH_Custom_" + text + "condition",
            DatabaseHelper.ConditionDefinitions.ConditionGrappledRestrainedRemorhaz,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text + "condition").ToString(),
            "MonsterAttack/&DH_" + text + "Condition" + "_Title",
            "MonsterAttack/&DH_" + text + "Condition" + "_Description"
        );

        //  TarrasqueGrappledRestrainedCondition.features.Add(DatabaseHelper.FeatureDefinitionActionAffinitys.ActionAffinityBlackTentacles);

        Tarrasque_Bite_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Remorhaz_Bite,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        Tarrasque_Bite_Attack.reachRange = 2;
        Tarrasque_Bite_Attack.toHitBonus = 19;
        Tarrasque_Bite_Attack.EffectDescription.hasSavingThrow = true;
        // using dex because dex is generally equivalent to or higher than str for most classes
        Tarrasque_Bite_Attack.EffectDescription.savingThrowAbility = DatabaseHelper.SmartAttributeDefinitions
            .Dexterity.name;
        Tarrasque_Bite_Attack.EffectDescription.difficultyClassComputation =
            RuleDefinitions.EffectDifficultyClassComputation.FixedValue;
        Tarrasque_Bite_Attack.EffectDescription.fixedSavingThrowDifficultyClass = 20;

        Tarrasque_Bite_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 4;
        Tarrasque_Bite_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D12;
        Tarrasque_Bite_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 10;
        Tarrasque_Bite_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypePiercing;

        Tarrasque_Bite_Attack.EffectDescription.EffectForms[2].ConditionForm
            .conditionDefinition = TarrasqueGrappledRestrainedCondition;
        Tarrasque_Bite_Attack.EffectDescription.EffectForms[2].canSaveToCancel = true;
        Tarrasque_Bite_Attack.EffectDescription.EffectForms.RemoveAt(1);


        // Tarrasque_Bite_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper.MonsterAttackDefinitions.Attack_BrownBear_Bite.EffectDescription.EffectParticleParameters);
    }

    public static void BuildNewTarrasque_Claw_Attack()
    {
        /*
         * Claw.
           Melee Weapon Attack:
           +19 to hit,
           reach 15 ft.,
           one target.
           Hit: 28 (4d8 + 10) slashing damage.
        */
        var text = "Tarrasque_Claw";


        Tarrasque_Claw_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Green_Dragon_Claw,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        Tarrasque_Claw_Attack.reachRange = 3;
        Tarrasque_Claw_Attack.toHitBonus = 19;

        Tarrasque_Claw_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 4;
        Tarrasque_Claw_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D8;
        Tarrasque_Claw_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 10;
        Tarrasque_Claw_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeSlashing;

        Tarrasque_Claw_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper.MonsterAttackDefinitions
            .Attack_Green_Dragon_Claw.EffectDescription.EffectParticleParameters);
    }

    public static void BuildNewTarrasque_Tail_Attack()
    {
        /*
         *  Tail.
            Melee Weapon Attack:
            +19 to hit,
            reach 20 ft.,
            one target.
            Hit: 24 (4d6 + 10) bludgeoning damage.
            If the target is a creature, it must succeed on a DC 20 Strength saving throw or be knocked prone.
        */
        var text = "Tarrasque_Tail";


        Tarrasque_Tail_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Green_Dragon_Tail,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        Tarrasque_Tail_Attack.reachRange = 4;
        Tarrasque_Tail_Attack.toHitBonus = 19;


        Tarrasque_Tail_Attack.EffectDescription.savingThrowAbility = DatabaseHelper.SmartAttributeDefinitions
            .Strength.name;
        Tarrasque_Tail_Attack.EffectDescription.difficultyClassComputation =
            RuleDefinitions.EffectDifficultyClassComputation.FixedValue;
        Tarrasque_Tail_Attack.EffectDescription.fixedSavingThrowDifficultyClass = 20;

        Tarrasque_Tail_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 4;
        Tarrasque_Tail_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D6;
        Tarrasque_Tail_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 10;
        Tarrasque_Tail_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypeBludgeoning;

        MotionForm motionForm = new();
        motionForm.type = MotionForm.MotionType.FallProne;

        EffectForm effectForm = new();
        effectForm.applyLevel = EffectForm.LevelApplianceType.No;
        effectForm.levelMultiplier = 1;
        effectForm.levelType = RuleDefinitions.LevelSourceType.ClassLevel;
        effectForm.createdByCharacter = true;
        effectForm.FormType = EffectForm.EffectFormType.Motion;
        effectForm.motionForm = motionForm;
        effectForm.hasSavingThrow = true;
        effectForm.savingThrowAffinity = RuleDefinitions.EffectSavingThrowType.Negates;

        Tarrasque_Tail_Attack.EffectDescription.EffectForms.Add(effectForm);


        Tarrasque_Tail_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper.MonsterAttackDefinitions
            .Attack_Green_Dragon_Tail.EffectDescription.EffectParticleParameters);
    }

    public static void BuildNewTarrasque_Horn_Attack()
    {
        /*
        Horns.
        Melee Weapon Attack:
        +19 to hit,
        reach 10 ft.,
        one target.
        Hit: 32 (4d10 + 10) piercing damage.
        */
        var text = "Tarrasque_Horn";


        Tarrasque_Horn_Attack = BuildNewAttack(
            "DH_Custom_" + text,
            DatabaseHelper.MonsterAttackDefinitions.Attack_Minotaur_Gore,
            GuidHelper.Create(new Guid(MonsterContext.GUID), "DH_Custom_" + text).ToString(),
            "MonsterAttack/&DH_" + text + "_Title",
            "MonsterAttack/&DH_" + text + "_Description"
        );

        Tarrasque_Horn_Attack.reachRange = 2;
        Tarrasque_Horn_Attack.toHitBonus = 19;

        Tarrasque_Horn_Attack.EffectDescription.EffectForms[0].DamageForm.diceNumber = 4;
        Tarrasque_Horn_Attack.EffectDescription.EffectForms[0].DamageForm.dieType = RuleDefinitions.DieType.D10;
        Tarrasque_Horn_Attack.EffectDescription.EffectForms[0].DamageForm.bonusDamage = 10;
        Tarrasque_Horn_Attack.EffectDescription.EffectForms[0].DamageForm
            .damageType = RuleDefinitions.DamageTypePiercing;

        Tarrasque_Horn_Attack.EffectDescription.EffectParticleParameters.Copy(DatabaseHelper.MonsterAttackDefinitions
            .Attack_Green_Dragon_Bite.EffectDescription.EffectParticleParameters);
    }

    //************************************************************************************************************************************
    //************************************************************************************************************************************
    public static MonsterAttackDefinition BuildNewAttack(string name, MonsterAttackDefinition baseAttack,
        string guid, string title, string description)
    {
        return MonsterAttackDefinitionBuilder
            .Create(baseAttack, name, guid)
            .SetOrUpdateGuiPresentation(title, description)
            .AddToDB();
    }

    public static ConditionDefinition BuildNewCondition(string name, ConditionDefinition baseCondition, string guid,
        string title, string description)
    {
        return ConditionDefinitionBuilder
            .Create(baseCondition, name, guid)
            .SetOrUpdateGuiPresentation(title, description)
            .AddToDB();
    }
}
