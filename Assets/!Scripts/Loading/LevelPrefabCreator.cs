using SaveSystem;
using UnityEngine;
using Zenject;

namespace Loading
{
    public class LevelPrefabCreator : MonoBehaviour
    {
        private SaveManager _saveManager;
        
        [Inject]
        public void Constructor(SaveManager saveManager)
        {
            _saveManager = saveManager;
        }

        public async void CreateLevelPrefab()
        {
            Transform position = GameObject.FindGameObjectWithTag("LevelPrefabTag").transform;
            
            var prefabLoadTask = AddressableLoader.LoadPrefabAsync(_saveManager.PlayerData.LastLevelPrefab);

            await prefabLoadTask;
            
            if (prefabLoadTask.IsFaulted || prefabLoadTask.Result == null)
            {
                Debug.LogError("Ошибка загрузки префаба.");
            }
            else
            {
                GameObject levelPrefab = prefabLoadTask.Result;
                Instantiate(levelPrefab, position);
                AddressableLoader.ReleasePrefab();
            }
        }
    }
}