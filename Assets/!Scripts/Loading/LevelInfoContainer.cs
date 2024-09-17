using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loading
{
    public static class LevelInfoContainer
    {
        private const int MaxAmountLevelPrefab = 2; 
        
        public static int GetRandomLevelNumber() => Random.Range(1, (SceneManager.sceneCountInBuildSettings));

        public static string GetRandomLevelPrefab() => $"Level{GenerateRandomLevel()}";

        private static int GenerateRandomLevel() => Random.Range(1, MaxAmountLevelPrefab);
    }
}