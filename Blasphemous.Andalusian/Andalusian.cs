using Blasphemous.ModdingAPI;
using I2.Loc;
using System.Collections.Generic;

namespace Blasphemous.Andalusian;

/// <summary>
/// Handles loading andalusian text and adding it to localization manager
/// </summary>
public class Andalusian : BlasMod
{
    internal Andalusian() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    private readonly Dictionary<string, string> andalusianLanguage = new();

    /// <summary>
    /// Load and replace text
    /// </summary>
    protected override void OnInitialize()
    {
        LoadText();
        ReplaceText();
    }

    private void ReplaceText()
    {
        int count = 0;

        foreach (LanguageSource source in LocalizationManager.Sources)
        {
            source.AddLanguage("Andalusian", "an");

            int lastLanguage = source.GetLanguages().Count - 1;
            foreach (string term in source.GetTermsList())
            {
                if (andalusianLanguage.TryGetValue(term, out string newText))
                {
                    source.GetTermData(term).Languages[lastLanguage] = newText;
                    count++;
                }
            }
        }

        Log($"Added {count} terms for Andalusian translation");
    }

    private void LoadText()
    {
        if (!FileHandler.LoadDataAsText("language.txt", out string text))
        {
            LogDisplay("Could not load the language.txt file!");
            return;
        }

        foreach (string line in text.Split('\n'))
        {
            int colonIdx = line.IndexOf(':');
            string key = line.Substring(0, colonIdx).Trim();
            string value = line.Substring(colonIdx + 1).Trim().Replace('@', '\n');

            if (value != string.Empty)
                andalusianLanguage.Add(key, value);
        }
    }
}
