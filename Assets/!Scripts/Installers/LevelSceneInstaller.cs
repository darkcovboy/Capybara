using Const;
using Items;
using Items.Collector;
using LevelStates;
using LevelStates.Data;
using LevelStates.TimerScripts;
using Player;
using Player.Counter;
using Player.Movement;
using Player.PlayerStaticData;
using SaveSystem;
using Sirenix.OdinInspector;
using UI.Screens;
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
        [SerializeField] private ScreensHolder _screensHolder;
        [SerializeField] private ItemCollector _itemCollector;
        [SerializeField] private GameState _game;

        public override void InstallBindings()
        {
            SetupTimer();
            SetupMovement();
            SetupMoneyCounter();
            SetupCollector();
            SetupStaticData();
            SetupPlayer();
            SetupGame();
            SetupUI();
        }

        private void SetupUI()
        {
            Container.Bind<VictoryScreen>().FromInstance(_screensHolder.VictoryScreen).AsSingle();
            Container.Bind<LoseScreen>().FromInstance(_screensHolder.LoseScreen).AsSingle();
        }

        private void SetupGame()
        {
            Container.Bind<GameState>().FromInstance(_game).AsSingle();
        }
        
        private void SetupPlayer()
        {
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