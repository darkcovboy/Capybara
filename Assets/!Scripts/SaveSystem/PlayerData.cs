using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Player.Skins;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    
    public class PlayerData
    {
        public int Money { get; set; }
        public int Level { get; set; }
        public int LastLevelId { get; set; }
        public string LastLevelPrefab { get; set; }
        public bool IsInstructionsCompleted { get; set; }
        public CharacterType SelectedSkin { get; set; }
        public List<CharacterType> UnlockedSkins { get; set; }
        public Dictionary<CharacterType, int> AdViewsForSkins { get; set; }
        public int Capacity { get; set; }
        public bool IsAllLevelsCompleted { get; set; }
        public int MoneyMultiplier { get; set; }


        public PlayerData()
        {
            Money = 0;
            Level = 1;
            LastLevelId = 1;
            LastLevelPrefab = "Level1";
            MoneyMultiplier = 1;
            IsInstructionsCompleted = false;
            SelectedSkin = CharacterType.CapybaraDefault;
            UnlockedSkins = new List<CharacterType> {SelectedSkin};
            Capacity = 3;
            AdViewsForSkins = new Dictionary<CharacterType, int>();
            IsAllLevelsCompleted = false;
        }
    }
}