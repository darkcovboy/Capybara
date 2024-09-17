using System.Collections.Generic;
using UI.ShopSkins;
using UnityEngine;

namespace UI.Screens
{
    public class ScreensHolder : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private LeaderboardScreen _leaderboardScreen;
        [SerializeField] private GameplayScreen _gameplayScreen;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private SettingsScreen _settingsScreen;
        [SerializeField] private LoseScreen _loseScreen;
        [SerializeField] private StartScreen _startScreen;

        private List<Screen> _screens = new List<Screen>();
        private Dictionary<ScreenType, Screen> _screenDictionary;

        public LoseScreen LoseScreen => _loseScreen;

        public VictoryScreen VictoryScreen => _victoryScreen;


        private void Start()
        {
            _screens.Add(_shop);
            _screens.Add(_leaderboardScreen);
            _screens.Add(_gameplayScreen);
            _screens.Add(_victoryScreen);
            _screens.Add(_settingsScreen);
            _screens.Add(_loseScreen);
            _screens.Add(_startScreen);
            
            _screenDictionary = new Dictionary<ScreenType, Screen>
            {
                { ScreenType.Shop, _shop },
                { ScreenType.Leaderboard, _leaderboardScreen },
                { ScreenType.Gameplay, _gameplayScreen },
                { ScreenType.Victory, _victoryScreen },
                { ScreenType.Settings, _settingsScreen },
                { ScreenType.Lose, _loseScreen },
                { ScreenType.Start, _startScreen}
            };
        }

        public void OpenScreen(Screen currentScreen)
        {
            foreach (var screen in _screens)
            {
                if (currentScreen == screen)
                    screen.Open();
                else
                    screen.Close();
            }
        }

        public void OpenScreen(ScreenType screenType)
        {
            if (_screenDictionary.TryGetValue(screenType, out var screenToOpen))
            {
                foreach (var screen in _screens)
                {
                    if (screen == screenToOpen)
                        screen.Open();
                    else
                        screen.Close();
                }
            }
        }
    }
    
    public enum ScreenType
    {
        Shop,
        Leaderboard,
        Gameplay,
        Victory,
        Settings,
        Lose,
        Start
    }
    
}