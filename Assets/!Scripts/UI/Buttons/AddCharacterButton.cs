using System;
using System.Collections.Generic;
using Const;
using DG.Tweening;
using Player;
using SaveSystem;
using StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Buttons
{
    public class AddCharacterButton : MonoBehaviour
    {
        [SerializeField] private Price _price;
        [SerializeField] private Button _button;
        [SerializeField] private float _lockAnimationDuration = 0.4f;
        [SerializeField] private float _lockAnimationStrength = 2f;
        
        private CharactersGroupHolder _player;
        private SaveManager _saveManager;
        private List<int> _prices;

        [Inject]
        public void Constructor(CharactersGroupHolder player, SaveManager saveManager)
        {
            _player = player;
            _saveManager = saveManager;
            _prices = Resources.Load<UpgradeCharacterPrices>(Paths.PricesHolderPath).Prices;
        }

        private void OnEnable()
        {
            UpdatePriceInfo();
            _button.onClick.AddListener(AddCharacter);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(AddCharacter);
        }

        private void UpdatePriceInfo()
        {
            if (_player.IsMaxCapacity())
            {
                _price.Show("Max");
            }
            else
            {
                _price.Show(_prices[_saveManager.PlayerData.Capacity]);
            }
        }

        private void AddCharacter()
        {
            if (!_player.IsMaxCapacity())
            {
                _saveManager.PlayerData.Capacity++;
                _player.AddCharacter();
                UpdatePriceInfo();
            }
            else
            {
                transform.DOShakePosition(_lockAnimationDuration, _lockAnimationStrength);
            }
        }
    }
}