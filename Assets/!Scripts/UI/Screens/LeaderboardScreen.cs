using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Leaderboard;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class LeaderboardScreen : Screen
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _container;
        [Header("Dependencies")]
        [SerializeField] private LeaderboardItemFabric _fabric;

        private List<LeaderboardItem> _leaderboardItems = new List<LeaderboardItem>();
        
        public void AnimatePlayers()
        {
            StartCoroutine(AnimateObject());
        }

        public void ClearInfo()
        {
            _leaderboardItems.Clear();
            
            foreach (Transform child in _container)
            {
                DestroyImmediate(child.gameObject);
            }
        }

        public void CreateLeaderboardItem(LeaderboardData leaderboardData, bool isPlayer)
        {
            LeaderboardItem leaderboardItem = _fabric.Get(_container);
            leaderboardItem.Init(leaderboardData, isPlayer);
            _leaderboardItems.Add(leaderboardItem);
        }

        private IEnumerator AnimateObject()
        {
            yield break;
        }
    }
}