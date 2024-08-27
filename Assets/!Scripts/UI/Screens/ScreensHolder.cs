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

        private List<Screen> _screens = new List<Screen>();

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
    }
}