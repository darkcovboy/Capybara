using System;
using Items;
using Items.Collector;
using LevelStates;
using LevelStates.Data;
using LevelStates.TimerScripts;
using Loading;
using Player;
using Player.Counter;
using Player.Movement;
using Player.PlayerStaticData;
using UI.Screens;
using UnityEngine;
using Zenject;
using Paths = Const.Paths;
using Timer = LevelStates.TimerScripts.Timer;

namespace Installers
{
    public class LevelSceneInstaller : MonoInstaller, ICoroutineRunner
    {
        [Header("Configs")]
        [SerializeField] private LevelData _levelData;
        [SerializeField] private MainPrefabHolder _mainPrefabHolder;

        [Header("Dependencies")] 
        [SerializeField] private LevelPrefabCreator _levelPrefabCreator;
        [SerializeField] private ScreensHolder _screensHolder;

        [Header("Positions")] 
        [SerializeField] private Transform _playerPosition;
        [SerializeField] private Transform _itemCollectorPosition;


        public override void InstallBindings()
        {
            SetupTimer();
            SetupMovement();
            SetupMoneyCounter();
            SetupCollector();
            SetupStaticData();
            SetupPlayer();
            SetupUI();
            SetupGame();
        }

        private void Awake()
        {
            SetupLevelPrefab();
        }

        private void SetupUI()
        {
            Container.Bind<ScreensHolder>().FromInstance(_screensHolder).AsSingle();
        }

        private void SetupGame()
        {
            Container.Bind<GameState>().AsSingle();
        }

        private void SetupLevelPrefab()
        {
            _levelPrefabCreator.CreateLevelPrefab();
        }

        private void SetupPlayer()
        {
            var player = Container.InstantiatePrefabForComponent<CharactersGroupHolder>(_mainPrefabHolder.CharactersGroupHolder, _playerPosition.position,
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
            var itemCollector = Container.InstantiatePrefabForComponent<ItemCollector>(_mainPrefabHolder.ItemCollector, _itemCollectorPosition.position,
                Quaternion.identity, null);
            
            Container.BindInterfacesAndSelfTo<ItemCollector>().FromInstance(itemCollector).AsSingle();
        }

        private void SetupMoneyCounter()
        {
            Container.BindInterfacesAndSelfTo<MoneyCounter>().AsSingle();
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