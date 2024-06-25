using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.ShopSkins
{
    [CreateAssetMenu(fileName = "Shop Content", menuName = "Shop Content")]
    public class ShopContent : ScriptableObject
    {
        [SerializeField] private List<CharacterSkinItem> _characterSkinsItems;

        public IEnumerable<CharacterSkinItem> CharacterSkins => _characterSkinsItems;

        private void OnValidate()
        {
            var animalSkinsDuplicates = _characterSkinsItems.GroupBy(item => item.CharacterType)
                .Where(array => array.Count() > 1);

            if (animalSkinsDuplicates.Count() > 0)
                throw new InvalidOperationException(nameof(_characterSkinsItems));
        }
    }
}