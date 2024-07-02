#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;

[Overlay(typeof(SceneView), "Play From Initial Scene")]
[Icon("d_PlayButton On")]
public class PlayFromInitialSceneButton : ToolbarOverlay
{
    private const string InitialScenePath = "Assets/Scenes/Initial.unity"; // Укажите путь к вашей начальной сцене

    public PlayFromInitialSceneButton() : base(PlayButton.id)
    {
    }

    [EditorToolbarElement(id, typeof(SceneView))]
    private class PlayButton : EditorToolbarButton
    {
        public const string id = "PlayFromInitialSceneButton/PlayButton";

        public PlayButton()
        {
            text = "Play From Initial Scene";
            icon = EditorGUIUtility.IconContent("d_PlayButton On").image as Texture2D;
            clicked += OnClick;
        }

        private void OnClick()
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
    }
}
#endif