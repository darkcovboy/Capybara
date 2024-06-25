using System;
using Player.Skins;
using UnityEngine;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
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
            //Реализовать сохранения
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
            //Реализовать загрузку
            return null;
        }
    }
}