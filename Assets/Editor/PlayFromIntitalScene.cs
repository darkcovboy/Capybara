using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor
{
    [InitializeOnLoad]
    public static class PlayFromIntitalScene
    {
        private const string InitialScenePath = "Assets/Scenes/Initial.unity";
        
        static PlayFromIntitalScene()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }
        
        [MenuItem("Custom/Play From Initial Scene")]
        public static void PlayFromInitialSceneMenu()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }

            // Сохраняем текущую сцену и открываем начальную сцену
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(InitialScenePath);

            // Запускаем игру
            EditorApplication.isPlaying = true;
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                // Если сцена уже загружена, ничего не делаем
                if (EditorSceneManager.GetActiveScene().path == InitialScenePath)
                {
                    return;
                }

                // Если игра запущена через кнопку Play в редакторе, переключаемся на начальную сцену
                if (!EditorApplication.isPlayingOrWillChangePlaymode && EditorApplication.isPlaying)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene(InitialScenePath);
                }
            }
        }

    }
}