using System;
using Extension;
using UI.ShopSkins;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class StartScreen : MonoBehaviour
    {
        [Header("Buttons")] 
        [SerializeField] private Button _gameplayStartButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _settingsButton;
        [Header("Screens")]
        [SerializeField] private GameplayScreen _gameplayScreen;
        [SerializeField] private LeaderboardScreen _leaderboardScreen;
        [SerializeField] private SettingsScreen _settingsScreen;
        [SerializeField] private Shop _shopScreen;

        private void OnEnable()
        {
            _gameplayStartButton.onClick.AddListener(_gameplayScreen.gameObject.Activate);
            _leaderboardButton.onClick.AddListener(_leaderboardScreen.gameObject.Activate);
            _shopButton.onClick.AddListener(_shopScreen.gameObject.Activate);
            _settingsButton.onClick.AddListener(_settingsScreen.gameObject.Activate);
        }

        private void OnDisable()
        {
            _gameplayStartButton.onClick.RemoveListener(_gameplayScreen.gameObject.Activate);
            _leaderboardButton.onClick.RemoveListener(_leaderboardScreen.gameObject.Activate);
            _shopButton.onClick.RemoveListener(_shopScreen.gameObject.Activate);
            _settingsButton.onClick.RemoveListener(_settingsScreen.gameObject.Activate);
        }
    }
}