using System;
using System.Collections.Generic;
using Ads;
using Localization;
using UnityEngine;
using YG;

namespace DefaultNamespace
{
    public class YandexMain : MonoBehaviour
    {
        public static YandexMain Instance { get; private set; }


        [field: SerializeField] public Language Language { get; private set; }
        [field: SerializeField] public bool IsMobile { get; private set; }


        public LocalisationManager LocalisationManager { get; private set; }
        public ADManager ADManager { get; private set; }

        public BackgroundFocus BackgroundFocus { get; private set; }

        private List<LocalisationDataSO> _localisationDatas = new List<LocalisationDataSO>();
        

        private void Awake()
        {
            if(Instance != null) return;

            Instance = this;
            DontDestroyOnLoad(this);

            DefinePlatformAndLanguage();
            CreateManagers();
            
            _localisationDatas.AddRange(LoadLocalisationDatas());
            LocalisationManager.CreateDictionary();
        }

        private void OnDestroy()
        {
            BackgroundFocus.Dispose();
        }

        private void DefinePlatformAndLanguage()
        {
            Language = GetLanguage();
            IsMobile = false;
            //Add defines
        }

        private Language GetLanguage()
        {
#if !UNITY_EDITOR
return YandexGame.lang == "ru" ? Language.Russian : Language.English;
#endif
            return Language;
        }

        private void CreateManagers()
        {
            LocalisationManager = new LocalisationManager(Language, _localisationDatas);
            ADManager = new ADManager();
            BackgroundFocus = new BackgroundFocus();
        }
        
        private LocalisationDataSO[] LoadLocalisationDatas() => Resources.LoadAll<LocalisationDataSO>("Localisation");

    }

    public enum Language
    {
        Russian,
        English
    }
}