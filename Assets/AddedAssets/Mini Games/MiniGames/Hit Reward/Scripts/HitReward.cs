using MiniGames;
using UnityEngine;

namespace HitRewardSpace
{
    public class HitReward : MiniGame
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private Animator _animator;

        [SerializeField] protected bool _randomSelection;


        public override void Init() {
            _gameController.SetPaidStartAmount(_paidStartPrice);
            _gameController.SetCoinIcon(_coinIcon);

            if (_useOneCurrency)
                _gameController.Init(_amounts, _randomOrder, _randomSelection);
            else
                _gameController.Init(_rewardSet, _randomOrder, _randomSelection);
        }

        [ContextMenu(nameof(Open))]
        public override void Open() {
            base.Open();

            _gameController.ResetGame();
            _gameController.WheelTurn();
            _animator.SetTrigger("Open");
        }

        [ContextMenu(nameof(Hide))]
        public override void Hide() {
            base.Hide();
        }
    }
}
