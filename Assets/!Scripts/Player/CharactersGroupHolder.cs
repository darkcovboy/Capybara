using System;
using System.Collections.Generic;
using Player.Movement;
using Player.PlayerStaticData;
using UnityEngine;
using Zenject;

namespace Player
{
    public class CharactersGroupHolder : MonoBehaviour
    {
        [Header("Character Settings")]
        [SerializeField] private CharacterPoint[] _positions;
        [SerializeField] private Character _prefab;
        [SerializeField] private int _count;

        private List<Character> _charactersList = new List<Character>();
        private CharacterData _characterData;
        private IInput _input;

        [Inject]
        public void Constructor(CharacterData characterData, IInput input)
        {
            _characterData = characterData;
            _input = input;
        }

        private void Start()
        {
            CreateCharacters();
        }

        private void CreateCharacters()
        {
            _charactersList.Clear();
            
            for (int i = 0; i < _count; i++)
            {
                if (!_positions[i].HaveCharacter)
                {
                    Character character = Instantiate(_prefab, _positions[i].transform);
                    character.Init(_characterData, _input);
                    _positions[i].SetCharacter(character);
                    _charactersList.Add(character);
                }
            }
        }
    }
}