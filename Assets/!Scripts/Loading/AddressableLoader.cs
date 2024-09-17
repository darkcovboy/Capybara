using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Loading
{
    public static class AddressableLoader
    {
        private static AsyncOperationHandle<GameObject> _prefabHandle;
        
        public static async Task<GameObject> LoadPrefabAsync(string prefabAddress)
        {
            _prefabHandle = Addressables.LoadAssetAsync<GameObject>(prefabAddress);
            
            await _prefabHandle.Task;

            if (_prefabHandle.Status == AsyncOperationStatus.Succeeded)
            {
                return _prefabHandle.Result;
            }

            Debug.LogError($"Ошибка загрузки префаба: {prefabAddress}");
            return null;
        }
        
        public static void ReleasePrefab()
        {
            if (_prefabHandle.IsValid())
            {
                Addressables.Release(_prefabHandle);
            }
        }
 
    }
}