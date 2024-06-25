using System.Collections.Generic;
using DefaultNamespace;

namespace Localization
{
    public class LocalisationManager
    {
        private readonly Language _language;
        private List<LocalisationDataSO> _localisationDatas;
        private Dictionary<string, LocalisationData> _dictionary = new Dictionary<string, LocalisationData>();

        public LocalisationManager(Language language, List<LocalisationDataSO> localisationDatas)
        {
            _language = language;
            _localisationDatas = localisationDatas;
        }
        
        public void CreateDictionary()
        {
            foreach (var itemLocalisationData in _localisationDatas)
            {
                foreach (var itemInData in itemLocalisationData.LocalisationList)
                {
                    _dictionary.TryAdd(itemInData.Key.ToUpper(), itemInData);
                }
            }
        }

        public string GetText(string key, params object[] tokens)
        {
            var text = _language == Language.Russian ? _dictionary[key.ToUpper()].RussianText : _dictionary[key.ToUpper()].EnglishText;

            for (int i = 0; i < tokens.Length; i++)
            {
                text = text.Replace($"{{T{i}}}", tokens[i].ToString());
            }

            return text;
        }
    }
}