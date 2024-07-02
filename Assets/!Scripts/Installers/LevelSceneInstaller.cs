using Const;
using Items;
using Items.Collector;
using LevelStates.Data;
using LevelStates.TimerScripts;
using Player;
using Player.Counter;
using Player.Movement;
using Player.PlayerStaticData;
using SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LevelSceneInstaller : MonoInstaller, ICoroutineRunner
    {
        [Header("Configs")]
        [SerializeField] private LevelData _levelData;
        [Header("Prefabs")] 
        [SerializeField] private CharactersGroupHolder _charactersGroupHolder;
        [Header("Positions")] 
        [SerializeField] private Transform _playerPosition;
        [Title("Dependencies")] 
        [SerializeField] private ItemCollector _itemCollector;

        public override void InstallBindings()
        {
            SetupSaveManager();
            SetupTimer();
            SetupMovement();
            SetupMoneyCounter();
            SetupCollector();
            SetupStaticData();
            SetupPlayer();
        }

        private void SetupSaveManager()
        {
            Container.Bind<SaveManager>().FromInstance(SaveManager.Instance).AsSingle();
        }

        private void SetupPlayer()
        {
            /*
            var player =Container.InstantiatePrefabForComponent<CharactersGroupHolder>(_charactersGroupHolder,
                _playerPosition.position, Quaternion.identity, null);
                */
            var player = Container.InstantiatePrefabForComponent<CharactersGroupHolder>(_charactersGroupHolder, _playerPosition.position,
                Quaternion.identity, null);

            Container.Bind<CharactersGroupHolder>().FromInstance(player).AsSingle();

            Container.Bind<IWatch>().To<PlayerBody>().FromInstance(player.gameObject.GetComponentInChildren<PlayerBody>())
                .AsSingle();
        }

        private void SetupTimer()
        {
            Timer timer = new Timer(this, _levelData.Time);
            Container.BindInterfacesAndSelfTo<Timer>().FromInstance(timer).AsSingle();
        }

        private void SetupCollector()
        {
            Container.BindInterfacesAndSelfTo<ItemCollector>().FromInstance(_itemCollector).AsSingle();
        }

        private void SetupMoneyCounter()
        {
            Container.BindInterfacesAndSelfTo<MoneyCounter>().AsSingle().WithArguments(10);
        }

        private void SetupMovement()
        {
            if(SystemInfo.deviceType == DeviceType.Handheld)
            {
                Container.BindInterfacesAndSelfTo<MobileInput>().AsSingle();
            }
            else
            {
                Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
            }
        }

        private void SetupStaticData()
        {
            CharacterData characterData = Resources.Load<CharacterData>(Paths.CharacterDataPath);
            Container.Bind<CharacterData>().FromInstance(characterData).AsSingle();
        }
    }
}