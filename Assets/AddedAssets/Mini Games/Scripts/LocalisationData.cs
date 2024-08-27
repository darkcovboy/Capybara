using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames
{
    [Serializable]
    public class LocalisationData
    {
        public string Key;
        public List<TextInLanguage> TextInLanguages = new();
        private Dictionary<Language, string> _dictionary;


        public string GetText(Language language) {
            if (_dictionary == null)
                CreateDictionary();

            if (_dictionary.ContainsKey(language))
                return _dictionary[language];
            else
                return _dictionary[Language.English];
        }

        public void CreateDictionary() {
            _dictionary = new();

            foreach (var item in TextInLanguages)
                _dictionary.Add(item.Language, item.Text);
        }
    }

    [Serializable]
    public struct TextInLanguage
    {
        public Language Language;
        [TextArea(1, 3)]
        public string Text;
    }
}
