﻿using System;
using System.Collections.Generic;
using System.Linq;
using Const;
using LevelStates;
using Player.Movement;
using Player.PlayerStaticData;
using Player.Skins;
using SaveSystem;
using UnityEngine;
using Zenject;

namespace Player
{
    public class CharactersGroupHolder : MonoBehaviour, ILoseNotification
    {
        public event Action OnGameLose;
        public event Action<Vector3> OnCharacterAdded;

        [Header("Character Settings")]
        [SerializeField] private CharacterPoint[] _positions;
        [SerializeField] private int _count;

        private List<Character> _charactersList = new List<Character>();
        private CharacterData _characterData;
        private IInput _input;
        private SaveManager _saveManager;
        private SkinsHolder _skinsHolder;

        public bool IsMaxCapacity() => _saveManager.PlayerData.Capacity == _positions.Length;
        
        [Inject]
        public void Constructor(CharacterData characterData, IInput input, SaveManager saveManager)
        {
            _characterData = characterData;
            _input = input;
            _saveManager = saveManager;
            _skinsHolder = Resources.Load<SkinsHolder>(Paths.SkinsHolderPath);
        }

        private void Start()
        {
            CreateCharacters();
        }

        public void Win()
        {
            foreach (var character in _charactersList)
            {
                character.Win();
            }
        }

        public void SkinChange()
        {
            CreateCharacters();
        }

        public void AddCharacter()
        {
            Character prefab = GetCurrentSkinPrefab();
            CharacterPoint point = _positions.FirstOrDefault(x => !x.HaveCharacter);
            Character character = Instantiate(prefab, point.transform);
            character.Init(_characterData, _input);
            point.SetCharacter(character);
            _charactersList.Add(character);
            character.OnDestroy += DeleteCharacter;
            OnCharacterAdded?.Invoke(point.transform.position);
        }

        private Character GetCurrentSkinPrefab()
        {
            CharacterType type = _saveManager.PlayerData.SelectedSkin;
            Character prefab = _skinsHolder.CharactersContainer.FirstOrDefault(x => x.CharacterType == type)?.Prefab;
            return prefab;
        }

        private void CreateCharacters()
        {
            var prefab = GetCurrentSkinPrefab();
            _charactersList.Clear();
            
            for (int i = 0; i < _count; i++)
            {
                if (!_positions[i].HaveCharacter)
                {
                    Character character = Instantiate(prefab, _positions[i].transform);
                    character.Init(_characterData, _input);
                    _positions[i].SetCharacter(character);
                    _charactersList.Add(character);
                    character.OnDestroy += DeleteCharacter;
                }
            }
        }

        private void DeleteCharacter(Character character)
        {
            _charactersList.Remove(character);

            if (_charactersList.Count == 0)
            {
                OnGameLose?.Invoke();
            }
        }
    }
}