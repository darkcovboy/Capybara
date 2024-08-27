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
        [Header("Screens")]
        [SerializeField] private GameplayScreen _gameplayScreen;
        [SerializeField] private LeaderboardScreen _leaderboardScreen;
        [SerializeField] private SettingsScreen _settingsScreen;
        [SerializeField] private Shop _shopScreen;
        
        private CharactersGroupHolder _player;
        private GameState _gameState;

        [Inject]
        public void Constructor(CharactersGroupHolder player, GameState gameState)
        {
            _player = player;
            _gameState = gameState;
        }

        private void OnEnable()
        {
            _leaderboardButton.onClick.AddListener(_leaderboardScreen.gameObject.Activate);
            _shopButton.onClick.AddListener(_shopScreen.gameObject.Activate);
            _settingsButton.onClick.AddListener(_settingsScreen.gameObject.Activate);
            _startGameplayButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            gameObject.SetActive(false);
            _gameplayScreen.gameObject.Activate();
            _gameState.StartGame();
        }

        private void OnDisable()
        {
            _leaderboardButton.onClick.RemoveListener(_leaderboardScreen.gameObject.Activate);
            _shopButton.onClick.RemoveListener(_shopScreen.gameObject.Activate);
            _settingsButton.onClick.RemoveListener(_settingsScreen.gameObject.Activate);
            _startGameplayButton.onClick.AddListener(StartGame);
        }
    }
}