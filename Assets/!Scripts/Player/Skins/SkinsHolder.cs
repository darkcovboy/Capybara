using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Skins
{
    [CreateAssetMenu(fileName = "SkinsContainer", menuName = "Skins/Container", order = 0)]
    public class SkinsHolder : ScriptableObject
    {
        [SerializeField] private List<Skin> _charactersContainer;

        public List<Skin> CharactersContainer => _charactersContainer;
    }

    [Serializable]
    public class Skin
    {
        public CharacterType CharacterType;
        public Character Prefab;

        public Skin(CharacterType characterType, Character prefab)
        {
            CharacterType = characterType;
            Prefab = prefab;
        }
    }
}