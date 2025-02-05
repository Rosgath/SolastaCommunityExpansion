﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using I2.Loc;
using SolastaCommunityExpansion.Models;
using SolastaCommunityExpansion.Utils;
#if DEBUG
using SolastaCommunityExpansion.Patches.Diagnostic;
#endif

namespace SolastaCommunityExpansion.Patches;

[HarmonyPatch(typeof(GameManager), "BindPostDatabase")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class GameManager_BindPostDatabase
{
    private static readonly HashSet<string> SupportedLanguages = new() {"zh-CN"};

    internal static void Postfix()
    {
#if DEBUG
        ItemDefinitionVerification.Load();
        EffectFormVerification.Load();
#endif

        // Translations must load first
        var currentLanguageCode = !SupportedLanguages.Contains(LocalizationManager.CurrentLanguageCode)
            ? Translations.English
            : LocalizationManager.CurrentLanguageCode;

        Translations.LoadTranslations(currentLanguageCode);

        // Resources must load second
        ResourceLocatorContext.Load();

        // Cache TA definitions for diagnostics and export
        DiagnosticsContext.CacheTaDefinitions();

        // Needs to be after CacheTADefinitions
        CeContentPackContext.Load();

        // Cache all Merchant definitions and what item types they sell
        MerchantTypeContext.Load();

        // These can be loaded in any order so we bump them at the beginning
        AdditionalNamesContext.Load();
        BugFixContext.Load();
        CharacterExportContext.Load();
        ConjurationsContext.Load();
        CustomReactionsContext.Load();
        CustomWeaponsContext.Load();
        DmProEditorContext.Load();
        FaceUnlockContext.Load();
        FlexibleBackgroundsContext.Switch();
        GameUiContext.Load();
        InitialChoicesContext.Load();
        InventoryManagementContext.Load();
        ItemCraftingContext.Load();
        Level20Context.Load();
        PickPocketContext.Load();
        PowerBundleContext.Load();
        RemoveBugVisualModelsContext.Load();
        RespecContext.Load();
        ShieldStrikeContext.Load();

        // Item Options must be loaded after Item Crafting
        ItemOptionsContext.Load();

        // Fighting Styles must be loaded before feats to allow feats to generate corresponding fighting style ones.
        FightingStyleContext.Load();

        // Powers needs to be added to db before spells because of summoned creatures that have new powers defined here.
        PowersContext.Load();

        // There are spells that rely on new monster definitions with powers loaded during the PowersContext. So spells should get added to db after powers.
        SpellsContext.Load();

        // Races may rely on spells and powers being in the DB before they can properly load.
        RacesContext.Load();

        // Classes may rely on spells and powers being in the DB before they can properly load.
        ClassesContext.Load();

        // Subclasses may rely on classes being loaded (as well as spells and powers) in order to properly refer back to the class.
        SubclassesContext.Load();

        // Multiclass blueprints should always load to avoid issues with heroes saves and after classes and subclasses
        MulticlassContext.Load();

        // Load SRD and House rules last in case they change previous blueprints
        SrdAndHouseRulesContext.Load();

        ServiceRepository.GetService<IRuntimeService>().RuntimeLoaded += _ =>
        {
            // Late initialized to allow feats and races from other mods
            FlexibleRacesContext.LateLoad();
            InitialChoicesContext.LateLoad();

            // There are feats that need all character classes loaded before they can properly be setup.
            FeatsContext.LateLoad();

            // Generally available powers need all classes in the db before they are initialized here.
            PowersContext.LateLoad();

            // Spells context needs character classes (specifically spell lists) in the db in order to do it's work.
            SpellsContext.LateLoad();

            // Integration Context
            IntegrationContext.LateLoad();

            // Divine Smite fixes
            HouseFeatureContext.LateLoad();

            // Level 20
            Level20Context.LateLoad();

            // Multiclass
            MulticlassContext.LateLoad();

            // Classes Features Sorting
            ClassesContext.LateLoad();

            // Save by location initialization depends on services to be ready
            SaveByLocationContext.LateLoad();

            // Recache all gui collections
            GuiWrapperContext.Recache();

            // Cache CE definitions for diagnostics and export
            DiagnosticsContext.CacheCeDefinitions();

            Main.Enable();

            // Manages update or welcome messages
            BootContext.Load();

            DelegatesContext.Load();
        };
    }
}
