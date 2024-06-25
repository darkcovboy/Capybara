using System;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class PlayerData
    {
        public int Money { get; set; }

        public void AddMoney(int value) => Money += value;
    }
}