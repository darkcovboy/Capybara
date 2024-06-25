using Const;
using Items;
using Items.Collector;
using Player;
using Player.Counter;
using Player.Movement;
using Player.PlayerStaticData;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LevelSceneInstaller : MonoInstaller
    {
        [Title("Dependencies")] 
        [SerializeField] private ItemCollector _itemCollector;
        [SerializeField] private PlayerBody _playerBody;
        
        public override void InstallBindings()
        {
            SetupMovement();
            SetupMoneyCounter();
            SetupCollector();
            SetupStaticData();
            SetupBody();
        }

        private void SetupCollector()
        {
            //Container.Bind<ItemCollector>().FromInstance(_itemCollector).AsSingle();
        }

        private void SetupMoneyCounter()
        {
            Container.Bind<MoneyCounter>().AsSingle().WithArguments(10);
        }

        private void SetupBody()
        {
            Container.Bind<IWatch>().To<PlayerBody>().FromInstance(_playerBody).AsSingle();
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