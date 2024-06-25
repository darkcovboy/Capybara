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
        public int Capacity { get; set; }
    }
}