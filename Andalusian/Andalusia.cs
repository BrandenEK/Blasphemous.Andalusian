using I2.Loc;
using ModdingAPI;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

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
            Framework.Managers.Core.Localization.SetLanguageByIdx(0);
            Log("Loaded and replaced Andalusian text!");
        }

        protected override void Update()
        {
            //if (UnityEngine.Input.GetKeyDown(KeyCode.P))
            //{
            //    ExportText();
            //}
            //if (UnityEngine.Input.GetKeyDown(KeyCode.O))
            //{
            //    Core.Localization.SetLanguageByIdx(0);
            //    LogWarning("Setting language");
            //}
        }

        void ReplaceText()
        {
            int count = 0;
            foreach (LanguageSource source in LocalizationManager.Sources)
            {
                foreach (string term in source.GetTermsList())
                {
                    if (andalusianLanguage.TryGetValue(term, out string newText))
                    {
                        source.GetTermData(term).Languages[0] = newText;
                        count++;
                    }
                }
            }

            Log($"Replaced {count} terms with Andalusian translation");
        }

        void ExportText()
        {
            StringBuilder terms = new(), spanish = new(), english = new();
            foreach (LanguageSource source in LocalizationManager.Sources)
            {
                foreach (string term in source.GetTermsList())
                {
                    LogWarning(term);
                    TermData data = source.GetTermData(term);
                    terms.AppendLine(term);
                    spanish.AppendLine(data.Languages[0].Replace('\n', '@').Replace('\r', '@'));
                    english.AppendLine(data.Languages[1].Replace('\n', '@').Replace('\r', '@'));
                }
            }

            FileUtil.saveTextFile("terms.txt", terms.ToString());
            FileUtil.saveTextFile("spanish.txt", spanish.ToString());
            FileUtil.saveTextFile("english.txt", english.ToString());
        }

        void LoadText()
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

                andalusianLanguage.Add(key, value);
            }
        }
    }
}
