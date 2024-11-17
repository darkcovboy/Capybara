using UnityEngine;

namespace DefaultNamespace.Leaderboard
{
    [CreateAssetMenu(fileName = "LeaderboardFabric", menuName = "UI/LeaderboardFabric")]
    public class LeaderboardItemFabric : ScriptableObject
    {
        [SerializeField] private LeaderboardItem _prefab;

        public LeaderboardItem Get(Transform container)
        {
            return Instantiate(_prefab, container);
        }
    }
}