using Extension;
using LevelStates;
using Player;
using UI.ShopSkins;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Screens
{
    public class StartScreen : Screen
    {
        [Header("Buttons")] 
        [SerializeField] private Button _startGameplayButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _settingsButton;
        
        private GameState _gameState;
        private ScreensHolder _screensHolder;

        [Inject]
        public void Constructor(ScreensHolder screensHolder, GameState gameState)
        {
            _screensHolder = screensHolder;
            _gameState = gameState;
        }

        private void OnEnable()
        {
            _leaderboardButton.onClick.AddListener(() => _screensHolder.OpenScreen(ScreenType.Leaderboard));
            _shopButton.onClick.AddListener(() => _screensHolder.OpenScreen(ScreenType.Shop));
            _settingsButton.onClick.AddListener(() => _screensHolder.OpenScreen(ScreenType.Settings));
            _startGameplayButton.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            _leaderboardButton.onClick.RemoveListener(() => _screensHolder.OpenScreen(ScreenType.Leaderboard));
            _shopButton.onClick.RemoveListener(() => _screensHolder.OpenScreen(ScreenType.Shop));
            _settingsButton.onClick.RemoveListener(() => _screensHolder.OpenScreen(ScreenType.Settings));
            _startGameplayButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _screensHolder.OpenScreen(ScreenType.Gameplay);
            _gameState.StartGame();
        }
    }
}