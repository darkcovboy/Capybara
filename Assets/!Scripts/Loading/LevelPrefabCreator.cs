using System.Threading.Tasks;
using SaveSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
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

        public async Task<GameObject> CreateLevelPrefab(Transform levelPrefabPosition)
        {
            // Загрузим префаб синхронно
            //Debug.Log(_saveManager.PlayerData.LastLevelPrefab);
            var handle = Addressables.LoadAssetAsync<GameObject>("Level2");

            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Failed || handle.Result == null)
            {
                Debug.LogError("Ошибка загрузки префаба.");
                return null;
            }

            GameObject levelPrefab = handle.Result;
            Addressables.Release(handle);
            return Instantiate(levelPrefab, levelPrefabPosition);
        }
    }
}