﻿using System;
using System.Linq;
using ModKit;
using SolastaCommunityExpansion.Api.Infrastructure;
using SolastaCommunityExpansion.Models;
using SolastaCommunityExpansion.Utils;

namespace SolastaCommunityExpansion.Displays;

public static class TranslationsDisplay
{
    internal static readonly string[] UnofficialLanguages = {"off", "es", "it"};

    internal static void DisplayTranslations()
    {
        UI.Label("");
        UI.Label(Gui.Format("ModUi/&Campaigns"));
        UI.Label("");

        var toggle = Main.Settings.EnableOnTheFlyTranslations;
        if (UI.Toggle(Gui.Localize("ModUi/&EnableOnTheFlyTranslations"), ref toggle, UI.AutoWidth()))
        {
            Main.Settings.EnableOnTheFlyTranslations = toggle;
        }

        UI.Label("");

        using (UI.HorizontalScope())
        {
            UI.Label(Gui.Localize("ModUi/&TargetLanguage"), UI.Width(120));

            var intValue = Array.IndexOf(Translations.AvailableLanguages, Main.Settings.SelectedLanguageCode);

            if (UI.SelectionGrid(
                    ref intValue,
                    Translations.AvailableLanguages,
                    Translations.AvailableLanguages.Length,
                    3, UI.Width(300)))
            {
                Main.Settings.SelectedLanguageCode = Translations.AvailableLanguages[intValue];
            }
        }

        UI.Label("");
        UI.Label(Gui.Localize("ModUi/&ExecuteBatchTranslations"));
        UI.Label("");

        var userCampaignPoolService = ServiceRepository.GetService<IUserCampaignPoolService>();

        foreach (var userCampaign in userCampaignPoolService.AllCampaigns
                     .Where(x => !x.TechnicalInfo.StartsWith(UserCampaignsTranslatorContext.Ce2TranslationTag))
                     .OrderBy(x => x.Title))
        {
            var exportName = userCampaign.Title;

            using (UI.HorizontalScope())
            {
                string buttonLabel;

                UI.Label(userCampaign.Author.Substring(0, Math.Min(16, userCampaign.Author.Length)).Bold().Orange(),
                    UI.Width(120));
                UI.Label(userCampaign.Title.Bold().Italic(), UI.Width(300));

                if (UserCampaignsTranslatorContext.CurrentExports.TryGetValue(exportName, out var status))
                {
                    buttonLabel = Gui.Format("ModUi/&TranslateCancel", status.LanguageCode.ToUpper(),
                        $"{status.PercentageComplete:00.0%}").Bold().Khaki();
                }
                else
                {
                    buttonLabel = Gui.Localize("ModUi/&Translate");
                }

                UI.ActionButton(buttonLabel, () =>
                    {
                        if (status == null)
                        {
                            UserCampaignsTranslatorContext.TranslateUserCampaign(
                                Main.Settings.SelectedLanguageCode, userCampaign.Title, userCampaign);
                        }
                        else
                        {
                            UserCampaignsTranslatorContext.Cancel(userCampaign.Title);
                        }
                    },
                    UI.Width(200));
            }
        }

        UI.Label("");
    }
}
