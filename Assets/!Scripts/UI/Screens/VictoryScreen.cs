using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class VictoryScreen : Screen
    {
        public event Action<float> OnRewardShowed;
        public event Action OnRewardNotShowed;
        
        [SerializeField] private RewardArrowMovement _rewardArrowMovement;
        [SerializeField] private TMP_Text _moneyEarned;
        [SerializeField] private Button _rewardButton;
        [SerializeField] private Button _nthxButton;

        public void Init(int earnedMoney)
        {
            _moneyEarned.text = $"{earnedMoney}";
            _rewardArrowMovement.SetFinalCoins(earnedMoney);
        }

        private void OnEnable()
        {
            _nthxButton.onClick.AddListener(NextLevel);
            _rewardButton.onClick.AddListener(ShowReward);
        }

        private void OnDisable()
        {
            _nthxButton.onClick.RemoveListener(NextLevel);
            _rewardButton.onClick.RemoveListener(ShowReward);
        }

        private void ShowReward()
        {
            YandexMain.Instance.ADManager.ShowRewardedAd(() =>
            {
                _rewardArrowMovement.enabled = false;
                float multiplier = _rewardArrowMovement.MultiplyFactor;
                _rewardArrowMovement.enabled = true;
                OnRewardShowed?.Invoke(multiplier);

            });
        }

        private void NextLevel()
        {
            OnRewardNotShowed?.Invoke();
        }
    }
}