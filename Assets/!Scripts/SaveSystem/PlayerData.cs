using System;
using System.Collections.Generic;
using Player.Skins;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class PlayerData
    {
        public int Money { get; set; }
        public CharacterType SelectedSkin { get; set; }
        public List<CharacterType> UnlockedSkins { get; set; }
        public Dictionary<CharacterType, int> AdViewsForSkins { get; set; }
        public int Capacity { get; set; }
        public bool IsAllLevelsCompleted { get; private set; }

        public PlayerData()
        {
            Money = 0;
            SelectedSkin = CharacterType.CapybaraDefault;
            UnlockedSkins = new List<CharacterType>();
            UnlockedSkins.Add(SelectedSkin);
            Capacity = 3;
            AdViewsForSkins = new Dictionary<CharacterType, int>();
            IsAllLevelsCompleted = false;
        }
    }
}