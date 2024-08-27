using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Loading
{
    public class LevelLoader : MonoBehaviour
    {
        public event Action<float> OnProgressChanged;
    
        public event Action OnSceneLoaded;
        
        [SerializeField] private LoadingCurtain _curtain;
        
        [Inject]
        private void Construct()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void LoadScene(int level)
        {
            if (level >= SceneManager.sceneCountInBuildSettings - 1)
            {
                level = GenerateRandomLevel();
            }

            StartCoroutine(LoadSceneAsync(level));
        }

        private IEnumerator LoadSceneAsync(int levelIndex)
        {
            _curtain.Show();

            AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                if (operation.progress >= 0.9f)
                {
                    OnProgressChanged?.Invoke(1f);
                    break;
                }
                
                OnProgressChanged?.Invoke(operation.progress /0.9f);

                yield return null;
            }

            operation.allowSceneActivation = true;
            
            yield return new WaitUntil(() => operation.isDone);

            _curtain.Hide();
            
            OnSceneLoaded?.Invoke();
        }

        private int GenerateRandomLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int totalScenes = SceneManager.sceneCountInBuildSettings;

            if (totalScenes <= 1)
            {
                throw new InvalidOperationException("Недостаточно уровней в настройках билда для выбора случайного уровня.");
            }

            int randomLevel;
            do
            {
                // Генерируем случайный индекс уровня в диапазоне от 1 до totalScenes - 1 (исключаем 0)
                randomLevel = UnityEngine.Random.Range(1, totalScenes);
            } while (randomLevel == currentSceneIndex); // Повторяем, если случайный уровень совпадает с текущим

            return randomLevel;
        }
    }
}