using Loading;
using SaveSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private SaveManager _saveManager;

        public override void InstallBindings()
        {
            Container.Bind<SaveManager>().FromInstance(_saveManager).AsSingle();
            Container.Bind<LevelLoader>().FromInstance(_levelLoader).AsSingle();
        }
    }
}