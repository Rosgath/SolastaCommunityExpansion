﻿using System.Collections.Generic;
using System.Linq;
using SolastaCommunityExpansion.Feats;

namespace SolastaCommunityExpansion.Models;

internal static class FeatsContext
{
    internal static HashSet<FeatDefinition> Feats { get; private set; } = new();

    internal static void LateLoad()
    {
        var feats = new List<FeatDefinition>();

        // Generate feats here and fill the list
        AcehighFeats.CreateFeats(feats);
        ArmorFeats.CreateArmorFeats(feats);
        CasterFeats.CreateFeats(feats);
        FightingStyleFeats.CreateFeats(feats);
        OtherFeats.CreateFeats(feats);
        HealingFeats.CreateFeats(feats);
        PickPocketContext.CreateFeats(feats);
        CraftyFeats.CreateFeats(feats);
        ElAntoniousFeats.CreateFeats(feats);
        ZappaFeats.CreateFeats(feats);
        EwFeats.CreateFeats(feats);

        feats.ForEach(LoadFeat);

        Feats = Feats.OrderBy(x => x.FormatTitle()).ToHashSet();
    }

    private static void LoadFeat(FeatDefinition featDefinition)
    {
        if (!Feats.Contains(featDefinition))
        {
            Feats.Add(featDefinition);
        }

        UpdateFeatsVisibility(featDefinition);
    }

    private static void UpdateFeatsVisibility(FeatDefinition featDefinition)
    {
        featDefinition.GuiPresentation.hidden = !Main.Settings.FeatEnabled.Contains(featDefinition.Name);
    }

    internal static void Switch(FeatDefinition featDefinition, bool active)
    {
        if (!Feats.Contains(featDefinition))
        {
            return;
        }

        var name = featDefinition.Name;

        if (active)
        {
            Main.Settings.FeatEnabled.TryAdd(name);
        }
        else
        {
            Main.Settings.FeatEnabled.Remove(name);
        }

        UpdateFeatsVisibility(featDefinition);
        GuiWrapperContext.RecacheFeats();
    }
}
