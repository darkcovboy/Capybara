using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Zenject;

namespace Loading
{
    public class LevelLoader : MonoBehaviour
    {
        public event Action<float> OnProgressChanged;
    
        public event Action OnSceneLoaded;
        
        [SerializeField] private LoadingCurtain _curtain;

        public void LoadScene(int level)
        {
            StartCoroutine(LoadSceneAsync(level));
        }

        private IEnumerator LoadSceneAsync(int levelIndex)
        {
            _curtain.Show();

            AsyncOperation sceneOperation = SceneManager.LoadSceneAsync(levelIndex);
            sceneOperation.allowSceneActivation = false;

            while (sceneOperation.progress < 0.9f)
            {
                OnProgressChanged?.Invoke(sceneOperation.progress / 0.9f);
                yield return null;
            }
            
            
            sceneOperation.allowSceneActivation = true;
            yield return new WaitUntil(() => sceneOperation.isDone);

            _curtain.Hide();
            OnSceneLoaded?.Invoke();
        }
    }
}