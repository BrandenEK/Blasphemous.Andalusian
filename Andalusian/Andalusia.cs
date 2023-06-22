using I2.Loc;
using ModdingAPI;
using System.Collections.Generic;

namespace Andalusian
{
    public class Andalusia : Mod
    {
        public Andalusia(string modId, string modName, string modVersion) : base(modId, modName, modVersion) { }

        private readonly Dictionary<string, string> andalusianLanguage = new();

        protected override void Initialize()
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
            if (!FileUtil.loadDataText("language.txt", out string text))
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
}
