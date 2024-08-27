using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "CharacterPrices", menuName = "StaticData/Prices", order = 0)]
    public class UpgradeCharacterPrices : ScriptableObject
    {
        [SerializeField] private List<int> _prices;

        public List<int> Prices => _prices;
    }
}