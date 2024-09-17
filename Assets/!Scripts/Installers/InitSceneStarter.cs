using Loading;
using SaveSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class InitSceneStarter : MonoBehaviour
    {
        [Inject]
        public void Constructor(SaveManager saveManager, LevelLoader levelLoader)
        {
            levelLoader.LoadScene(saveManager.PlayerData.LastLevelId);
        }
    }
}