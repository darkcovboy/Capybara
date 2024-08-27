using System;
using Newtonsoft.Json;
using Player;
using Player.Skins;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        private const string _key = "SaveKey";
        public static SaveManager Instance { get; private set; }

        public PlayerData PlayerData { get; private set; }

        private void Awake()
        {
            if(Instance != null) return;

            Instance = this;
            DontDestroyOnLoad(this);

            PlayerData = LoadData();
        }

        [Inject]
        public void Constructor()
        {
            PlayerData = LoadData();
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(PlayerData);
            PlayerPrefs.SetString(_key,json);
            PlayerPrefs.Save();
        }
        
        public void AddMoney(int value) => PlayerData.Money += value;

        public void OpenNewSkin(CharacterType characterType)
        {
            
        }

        public void SelectSkin(CharacterType characterType)
        {
            
        }

        private PlayerData LoadData()
        {
            string json = PlayerPrefs.GetString(_key);
            
            if(String.IsNullOrEmpty(json))
                return new PlayerData();
            
            return JsonConvert.DeserializeObject<PlayerData>(json);
        }
    }
}