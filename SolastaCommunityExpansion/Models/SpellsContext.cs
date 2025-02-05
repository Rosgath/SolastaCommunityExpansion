﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SolastaCommunityExpansion.Spells;

namespace SolastaCommunityExpansion.Models;

internal static class SpellsContext
{
    internal static readonly Dictionary<SpellListDefinition, SpellListContext> SpellListContextTab = new();

    // ReSharper disable once InconsistentNaming
    private static readonly SortedList<string, SpellListDefinition> spellLists = new();
    internal static HashSet<SpellDefinition> Spells { get; private set; } = new();

    [NotNull]
    internal static SortedList<string, SpellListDefinition> SpellLists
    {
        get
        {
            if (spellLists.Count != 0)
            {
                return spellLists;
            }

            var dbCharacterClassDefinition = DatabaseRepository.GetDatabase<CharacterClassDefinition>();
            var dbCharacterSubclassDefinition = DatabaseRepository.GetDatabase<CharacterSubclassDefinition>();

            foreach (var characterClass in dbCharacterClassDefinition)
            {
                var title = characterClass.FormatTitle();

                var featureDefinitionCastSpell = characterClass.FeatureUnlocks
                    .Select(x => x.FeatureDefinition)
                    .OfType<FeatureDefinitionCastSpell>()
                    .FirstOrDefault();

                // NOTE: don't use featureDefinitionCastSpell?. which bypasses Unity object lifetime check
                if (featureDefinitionCastSpell
                    && featureDefinitionCastSpell.SpellListDefinition
                    && !spellLists.ContainsValue(featureDefinitionCastSpell.SpellListDefinition))
                {
                    spellLists.Add(title, featureDefinitionCastSpell.SpellListDefinition);
                }
            }

            foreach (var characterSubclass in dbCharacterSubclassDefinition)
            {
                var title = characterSubclass.FormatTitle();

                var featureDefinition = characterSubclass.FeatureUnlocks
                    .Select(x => x.FeatureDefinition)
                    .FirstOrDefault(x => x is FeatureDefinitionCastSpell or FeatureDefinitionMagicAffinity);

                switch (featureDefinition)
                {
                    case FeatureDefinitionMagicAffinity featureDefinitionMagicAffinity
                        when featureDefinitionMagicAffinity.ExtendedSpellList != null &&
                             !spellLists.ContainsValue(featureDefinitionMagicAffinity.ExtendedSpellList):
                        spellLists.Add(title, featureDefinitionMagicAffinity.ExtendedSpellList);
                        break;
                    case FeatureDefinitionCastSpell featureDefinitionCastSpell
                        when featureDefinitionCastSpell.SpellListDefinition != null &&
                             !spellLists.ContainsValue(featureDefinitionCastSpell.SpellListDefinition):
                        spellLists.Add(title, featureDefinitionCastSpell.SpellListDefinition);
                        break;
                }
            }

            return spellLists;
        }
    }

    internal static bool IsAllSetSelected()
    {
        return SpellListContextTab.Values.All(spellListContext => spellListContext.IsAllSetSelected);
    }

    internal static bool IsSuggestedSetSelected()
    {
        return SpellListContextTab.Values.All(spellListContext => spellListContext.IsSuggestedSetSelected);
    }

    internal static void SelectAllSet(bool toggle)
    {
        foreach (var spellListContext in SpellListContextTab.Values)
        {
            spellListContext.SelectAllSetInternal(toggle);
        }
    }

    internal static void SelectSuggestedSet(bool toggle)
    {
        foreach (var spellListContext in SpellListContextTab.Values)
        {
            spellListContext.SelectSuggestedSetInternal(toggle);
        }
    }

    internal static void LateLoad()
    {
        // init collections
        foreach (var spellList in SpellLists.Values)
        {
            var name = spellList.Name;

            SpellListContextTab.Add(spellList, new SpellListContext(spellList));

            Main.Settings.SpellListSpellEnabled.TryAdd(name, new List<string>());
            Main.Settings.DisplaySpellListsToggle.TryAdd(name, false);
            Main.Settings.SpellListSliderPosition.TryAdd(name, 4);
        }

        // register spells
        AceHighSpells.Register();
        BazouSpells.Register();
        HolicSpells.Register();
        SrdSpells.Register();
        EwSpells.Register();
        HouseSpellTweaks.Register();
        SgSpells.Register();

        // caches which spells are toggleable per spell list
        Spells = Spells.OrderBy(x => x.SpellLevel).ThenBy(x => x.FormatTitle()).ToHashSet();

        foreach (var spellListContext in SpellListContextTab.Values)
        {
            spellListContext.CalculateAllSpells();
        }
    }

    internal static void Load()
    {
        //BazouSpells.AddToDB();
        HolicSpells.AddToDB();
        SrdSpells.AddToDB();
        SgSpells.AddToDB();
    }

    internal static void RegisterSpell(
        SpellDefinition spellDefinition,
        int suggestedStartsAt = 0,
        params SpellListDefinition[] registeredSpellLists)
    {
        if (Spells.Contains(spellDefinition))
        {
            return;
        }

        Spells.Add(spellDefinition);

        for (var i = 0; i < registeredSpellLists.Length; i++)
        {
            var spellList = registeredSpellLists[i];

            if (i < suggestedStartsAt)
            {
                SpellListContextTab[spellList].MinimumSpells.Add(spellDefinition);
            }
            else
            {
                var enable = Main.Settings.SpellListSpellEnabled[spellList.Name].Contains(spellDefinition.Name);

                SpellListContextTab[spellList].Switch(spellDefinition, enable);
                SpellListContextTab[spellList].SuggestedSpells.Add(spellDefinition);
            }
        }
    }

    internal sealed class SpellListContext
    {
        public SpellListContext(SpellListDefinition spellListDefinition)
        {
            SpellList = spellListDefinition;
            AllSpells = new HashSet<SpellDefinition>();
            MinimumSpells = new HashSet<SpellDefinition>();
            SuggestedSpells = new HashSet<SpellDefinition>();
        }

        private List<string> SelectedSpells => Main.Settings.SpellListSpellEnabled[SpellList.Name];
        private SpellListDefinition SpellList { get; }
        public HashSet<SpellDefinition> AllSpells { get; }
        public HashSet<SpellDefinition> MinimumSpells { get; }
        public HashSet<SpellDefinition> SuggestedSpells { get; }

        // ReSharper disable once MemberHidesStaticFromOuterClass
        public bool IsAllSetSelected => SelectedSpells.Count == AllSpells.Count;

        // ReSharper disable once MemberHidesStaticFromOuterClass
        public bool IsSuggestedSetSelected => SelectedSpells.Count == SuggestedSpells.Count
                                              && SuggestedSpells.All(x => SelectedSpells.Contains(x.Name));

        public void CalculateAllSpells()
        {
            var minSpellLevel = SpellList.HasCantrips ? 0 : 1;
            var maxSpellLevel = SpellList.MaxSpellLevel;

            AllSpells.Clear();

            foreach (var spell in Spells
                         .Where(x => x.SpellLevel >= minSpellLevel && x.SpellLevel <= maxSpellLevel &&
                                     !MinimumSpells.Contains(x)))
            {
                AllSpells.Add(spell);
            }
        }

        public void SelectAllSetInternal(bool toggle)
        {
            foreach (var spell in AllSpells)
            {
                Switch(spell, toggle);
            }
        }

        public void SelectSuggestedSetInternal(bool toggle)
        {
            if (toggle)
            {
                SelectAllSetInternal(false);
            }

            foreach (var spell in SuggestedSpells)
            {
                Switch(spell, toggle);
            }
        }

        public void Switch([NotNull] SpellDefinition spellDefinition, bool active)
        {
            var spellListName = SpellList.Name;
            var spellName = spellDefinition.Name;
            var spellList = SpellList.SpellsByLevel.Find(x => x.Level == spellDefinition.SpellLevel).Spells;

            if (active)
            {
                spellList.TryAdd(spellDefinition);
                Main.Settings.SpellListSpellEnabled[spellListName].TryAdd(spellName);
            }
            else
            {
                spellList.Remove(spellDefinition);
                Main.Settings.SpellListSpellEnabled[spellListName].Remove(spellName);
            }
        }
    }
}
