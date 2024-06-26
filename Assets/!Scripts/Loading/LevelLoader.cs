using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loading
{
    public class LevelLoader : MonoBehaviour
    {
        private void Awake()
        {
            LoadLevel();
        }

        private void LoadLevel()
        {
            SceneManager.LoadScene(1);
        }
    }
}