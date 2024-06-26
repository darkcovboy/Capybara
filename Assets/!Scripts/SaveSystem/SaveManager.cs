using System;
using Player.Skins;
using UnityEngine;

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
            if (!PlayerPrefs.HasKey(_key))
            {
                return new PlayerData();
            }

            string json = PlayerPrefs.GetString(_key);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}