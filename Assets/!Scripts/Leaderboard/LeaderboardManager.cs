using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UI.Screens;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Leaderboard
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private List<LeaderboardData> _testData;
        
        private LeaderboardScreen _leaderboardView;
        private ScreensHolder _screensHolder;
        private readonly int _playerId = 1620;
        private readonly int _limitPlayerName = 14;
        private readonly int _limitPlayers = 8;

        [Inject]
        public void Constructor(ScreensHolder screensHolder)
        {
            _screensHolder = screensHolder;
            _leaderboardView = screensHolder.LeaderboardScreen;
        }

        [Button]
        public void Show()
        {
            if (_testData.Count == 0)
            {
                Debug.LogWarning("players empty");
                return;
            }

            _leaderboardView.ClearInfo();
            foreach (var leaderboardData in _testData)
            {
                bool isPlayer = leaderboardData.id == _playerId;

                if (string.IsNullOrEmpty(leaderboardData.name))
                {
                    leaderboardData.name = "hidden";
                }

                if (leaderboardData.name.Length > _limitPlayerName)
                {
                    leaderboardData.name = leaderboardData.name.Remove(_limitPlayerName);
                    leaderboardData.name += "...";
                }
                
                _leaderboardView.CreateLeaderboardItem(leaderboardData, isPlayer);
            }
            _screensHolder.OpenScreen(ScreenType.Leaderboard);
            _leaderboardView.AnimatePlayers();
        }

        public void NewScore(float score)
        {
            //Add score
        }
    }

    [Serializable]
    public class LeaderboardData
    {
        public int id;
        public int rank;
        public Sprite avatar;
        public int score;
        public string name;
    }
}