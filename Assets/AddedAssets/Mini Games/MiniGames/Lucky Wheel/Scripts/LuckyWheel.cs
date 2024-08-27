using MiniGames;
using UnityEngine;

namespace FortuneWheel
{
    public class LuckyWheel : MiniGame
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private AudioSource _audioOpen;
        [SerializeField] private Animator _animator;

        [SerializeField] protected bool _randomSelection;


        public override void Init() {
            _gameController.SetPaidStart(_paidStartPrice);
            _gameController.SetCoinIcon(_coinIcon);

            if (_useOneCurrency)
                _gameController.Init(_amounts, _randomOrder, _randomSelection);
            else
                _gameController.Init(_rewardSet, _randomOrder, _randomSelection);
        }

        [ContextMenu(nameof(Open))]
        public override void Open() {
            base.Open();

            _audioOpen.Play();
            _animator.SetTrigger("Open");
        }

        [ContextMenu(nameof(Hide))]
        public override void Hide() {
            base.Hide();

            if (_gameController.IsInit)
                _gameController.ResetGame();
        }
    }
}
