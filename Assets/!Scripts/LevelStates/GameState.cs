using System;
using Items.Collector;
using LevelStates.TimerScripts;
using Loading;
using Player;
using Player.Counter;
using SaveSystem;
using UI.Screens;
using UnityEngine;
using Zenject;

namespace LevelStates
{
    public class GameState : IDisposable
    {
        private CharactersGroupHolder _player;
        private Timer _timer;
        private ItemCollector _itemCollector;
        private VictoryScreen _victoryScreen;
        private SaveManager _saveManager;
        private MoneyCounter _moneyCounter;
        private ScreensHolder _screensHolder;
        private LoseScreen _loseScreen;
        private LevelLoader _levelLoader;

        [Inject]
        public void Constructor(CharactersGroupHolder groupHolder,
            Timer timer,
            ItemCollector itemCollector,
            MoneyCounter moneyCounter,
            SaveManager saveManager,
            LevelLoader levelLoader,
            ScreensHolder screensHolder)
        {
            _player = groupHolder;
            _timer = timer;
            _itemCollector = itemCollector;
            _saveManager = saveManager;
            _moneyCounter = moneyCounter;
            _screensHolder = screensHolder;
            _victoryScreen = screensHolder.VictoryScreen;
            _loseScreen = screensHolder.LoseScreen;
            _levelLoader = levelLoader;
            Subscribe();

            void Subscribe()
            {
                _player.OnGameLose += LoseGame;
                _timer.OnGameLose += LoseGame;
                _itemCollector.OnGameWin += WinGame; 
                _victoryScreen.OnRewardShowed += ShowReward;
                _victoryScreen.OnRewardNotShowed += NextLevel;
                _loseScreen.OnReloadButtonPress += ReloadLevel;
            }
        }

        public void Dispose()
        {
            Unsubscribe();

            void Unsubscribe()
            {
                _player.OnGameLose -= LoseGame;
                _timer.OnGameLose -= LoseGame;
                _itemCollector.OnGameWin -= WinGame;
                _victoryScreen.OnRewardShowed -= ShowReward;
                _victoryScreen.OnRewardNotShowed -= NextLevel;
                _loseScreen.OnReloadButtonPress -= ReloadLevel;
            }
        }

        public void StartGame()
        {
            _player.UnblockMovement();
            _timer.Start();
        }

        private void ReloadLevel()
        {
            _levelLoader.LoadScene(_saveManager.PlayerData.Level);
        }

        private void NextLevel()
        {
            _saveManager.Save();
            if(_saveManager.PlayerData.LastLevelId % 5 == 0 )
                _saveManager.PlayerData.MoneyMultiplier++;
            _levelLoader.LoadScene(_saveManager.PlayerData.LastLevelId);
        }

        private void ShowReward(float multiplier)
        {
            _saveManager.PlayerData.Money += (int)(multiplier - 1) * _moneyCounter.LevelCollectedMoney;
            _saveManager.Save();
            NextLevel();
        }

        private void WinGame()
        {
            _timer.Stop();
            _player.Win();
            _victoryScreen.Init(_moneyCounter.LevelCollectedMoney);
            _screensHolder.OpenScreen(_victoryScreen);
            SaveProgress();
        }

        private void LoseGame()
        {
            _timer.Stop();
            _screensHolder.OpenScreen((_loseScreen));
        }

        private void SaveProgress()
        {
            if (_saveManager.PlayerData.LastLevelPrefab == "Training")
            {
                _saveManager.PlayerData.IsInstructionsCompleted = true;
            }

            _saveManager.PlayerData.LastLevelId = LevelInfoContainer.GetRandomLevelNumber();
            _saveManager.PlayerData.LastLevelPrefab = LevelInfoContainer.GetRandomLevelPrefab();
            _saveManager.PlayerData.Money += _moneyCounter.LevelCollectedMoney;
            _saveManager.PlayerData.Level++;
            _saveManager.Save();
        }
    }
}