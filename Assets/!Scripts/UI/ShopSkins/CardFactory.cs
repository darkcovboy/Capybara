using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.ShopSkins
{
    [CreateAssetMenu(fileName = "CardFactory", menuName = "Shop/CardFactory")]
    public class CardFactory : ScriptableObject
    {
        [SerializeField] private List<CardStar> _cardStars;

        public Card Get(int star)
        {
            return _cardStars.First(x => x.Star == star).Prefab;
        }
    }

    [Serializable]
    public class CardStar
    {
        public Card Prefab;
        public int Star;
    }
}