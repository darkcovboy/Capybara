using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames
{
    public class LocalisationManager : MonoBehaviour
    {
        [SerializeField] private List<LocalisationData> _localisationDatas;
        private Dictionary<string, LocalisationData> _dictionary;
        private List<LocalisationText> _texts = new();

        public static Language Language { get; private set; } = Language.English;
        private static event Action OnLanguageUpdate;


        public static void SetLanguage(Language language) {
            Language = language;
            OnLanguageUpdate?.Invoke();
        }

        private void Awake() {
            OnLanguageUpdate += UpdateAllTexts;
            CreateDictionary();
        }

        public void UpdateAllTexts() {
            foreach (var text in _texts)
                text.SetText(GetText(text.Key));
        }

        public void Subscribe(LocalisationText text) {
            if(_texts.Contains(text)) { Debug.LogError("Attempt to re-subscribe"); return; }

            _texts.Add(text);
            text.SetText(GetText(text.Key));
        }

        public void CreateDictionary() {
            _dictionary = new();

            foreach (var item in _localisationDatas) {
                _dictionary.Add(item.Key, item);
            }
        }

        public string GetText(string key, params object[] tokens) {
            string text = _dictionary[key].GetText(Language);

            for (int i = 0; i < tokens.Length; i++)
                text = text.Replace($"{{T{i}}}", tokens[i].ToString());

            return text;
        }

        private void OnDestroy() {
            OnLanguageUpdate -= UpdateAllTexts;
        }
    }
}
