using System;
using DG.Tweening;
using Player;
using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Buttons
{
    public class AddCharacterButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;
        [SerializeField] private float _lockAnimationDuration = 0.4f;
        [SerializeField] private float _lockAnimationStrength = 2f;
        
        private CharactersGroupHolder _player;
        private SaveManager _saveManager;

        [Inject]
        public void Constructor(CharactersGroupHolder player, SaveManager saveManager)
        {
            _player = player;
            _saveManager = saveManager;
        }

        private void OnEnable()
        {
            CheckInteractable();
            _button.onClick.AddListener(AddCharacter);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(AddCharacter);
        }

        private void CheckInteractable()
        {
            if (_player.IsMaxCapacity())
            {
                _text.text = "Max";
            }
        }

        private void AddCharacter()
        {
            if (!_player.IsMaxCapacity())
            {
                _saveManager.PlayerData.Capacity++;
                _player.AddCharacter();
            }
            else
            {
                transform.DOShakePosition(_lockAnimationDuration, _lockAnimationStrength);
            }
        }
    }
}