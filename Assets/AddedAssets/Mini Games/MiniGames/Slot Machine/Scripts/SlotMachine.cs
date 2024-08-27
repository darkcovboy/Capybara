using MiniGames;
using UnityEngine;

namespace SlotMachineSpace
{
    public class SlotMachine : MiniGame
    {
        [SerializeField] private GameController _gameController;
        [SerializeField, Range(0,100)] private int _chanceRatio = 50;


        public override void Init() {
            _gameController.SetPaidStart(_paidStartPrice);
            _gameController.SetCoinIcon(_coinIcon);

            if (_useOneCurrency)
                _gameController.Init(_amounts);
            else
                _gameController.Init(_rewardSet, _randomOrder);

            _gameController.SetChanceRatio(_chanceRatio);
        }

        public override void Open() {
            base.Open();
        }

        public override void Hide() {
            base.Hide();

            _gameController.ResrtGame();
        }
    }
}
