using System;
using System.Threading.Tasks;
using DefaultNamespace;
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
        [SerializeField] private Transform _levelPrefabPosition;
        [SerializeField] private Transform _playerPosition;
        [SerializeField] private Transform _itemCollectorPosition;
        private ItemCollector _itemCollector;
        private CharactersGroupHolder _player;


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

        private async void Awake()
        {
            await SetupLevelPrefab();
        }

        private void SetupUI()
        {
            Container.Bind<ScreensHolder>().FromInstance(_screensHolder).AsSingle();
        }

        private void SetupGame()
        {
            Container.Bind<GameState>().AsSingle();
        }

        private async Task SetupLevelPrefab()
        {
            GameObject levelPrefab = await  _levelPrefabCreator.CreateLevelPrefab(_levelPrefabPosition);
            Level level = levelPrefab.GetComponent<Level>();
            _player.transform.position = level.PlayerPosition.position;
            _player.CreateCharacters();
            _itemCollector.CalculateItems();
            _itemCollector.transform.position = level.CollectorPosition.position;
            Container.Inject(level.EnemyInstaller);

        }

        private void SetupPlayer()
        {
            _player = Container.InstantiatePrefabForComponent<CharactersGroupHolder>(_mainPrefabHolder.CharactersGroupHolder, _playerPosition.position,
                Quaternion.identity, null);

            Container.Bind<CharactersGroupHolder>().FromInstance(_player).AsSingle();

            Container.Bind<IWatch>().To<PlayerBody>().FromInstance(_player.gameObject.GetComponentInChildren<PlayerBody>())
                .AsSingle();
        }

        private void SetupTimer()
        {
            Timer timer = new Timer(this, _levelData.Time);
            Container.BindInterfacesAndSelfTo<Timer>().FromInstance(timer).AsSingle();
        }

        private void SetupCollector()
        {
            _itemCollector = Container.InstantiatePrefabForComponent<ItemCollector>(_mainPrefabHolder.ItemCollector, _itemCollectorPosition.position,
                Quaternion.identity, null);
            
            Container.BindInterfacesAndSelfTo<ItemCollector>().FromInstance(_itemCollector).AsSingle();
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