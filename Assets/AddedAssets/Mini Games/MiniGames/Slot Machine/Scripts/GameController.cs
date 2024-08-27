using MiniGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace SlotMachineSpace
{
    public class GameController : MonoBehaviour
    {
        [Header("Game Inputs")]
        [Space]
        [SerializeField] private Gateway _gateway;
        [SerializeField] private int _chanceRatio = 50;                 // Giving reward chance ratio
        [SerializeField] private float _delayBetweenWheels = 1f;
        [SerializeField] private float _delayToStop = 2;
        [SerializeField] private Column[] _wheels;
        [SerializeField] private RewardElement[] _rewardElements;
        [SerializeField] private List<Sprite> _sprites;
        [SerializeField] private CategoryIcon _coinIcon;

        [Header("UI Elements")]
        [Space]
        [SerializeField] private PopupPanel _popupPanel;
        [SerializeField] private CustomButton _pullButton;
        [SerializeField] private CustomButton _pullHandle;
        [SerializeField] private GameObject _frontTable;
        [SerializeField] private PaidStartElement _paidStartElement;
        [SerializeField] private PanelLaunchFromAd _panelLaunchFromAd;

        [Header("Effect Settings")]
        [Space]
        [SerializeField] private ParticleSystem _confetiEffect;
        private UIParticleSystem _uiParticleSystem;

        private int _currentRewardIndex;
        private List<Reward> _rewards = new();
        private int[] _amounts;
        private bool _useOneCurrency;


        /// <param name="set">List of rewards</param>
        /// <param name="randomOrder">Rewards are placed in a random order</param>
        public void Init(RewardSet set, bool randomOrder = false) {
            Init(set.Rewards, randomOrder);
        }

        /// <param name="rewards">List of rewards</param>
        /// <param name="randomOrder">Rewards are placed in a random order</param>
        public void Init(List<Reward> rewards = null, bool randomOrder = false) {
            if (rewards == null || rewards.Count == 0) { Debug.Log(rewards); Debug.Log(rewards.Count); Debug.LogError("Awards data have not been assigned"); return; }
            _useOneCurrency = false;
            _frontTable.SetActive(false);

            List<Reward> rewardsContainer = new();
            for (int i = 0; i < rewards.Count; i++) {
                if (rewardsContainer.Count == 0)
                    rewardsContainer.AddRange(rewards);

                int index = 0;
                if (randomOrder)
                    index = Random.Range(0, rewardsContainer.Count);

                Reward reward = rewardsContainer[index];
                reward.ResetCurrentAmount();
                _rewards.Add(reward.Clone());

                rewardsContainer.RemoveAt(index);
            }

            List<Sprite> sprites = new();
            foreach (Reward reward in _rewards)
                sprites.Add(reward.Icons.Sprite);

            foreach (var wheel in _wheels)
                wheel.Init(sprites);

            Init();
        }

        /// <param name="amounts">List of rewards</param>
        public void Init(int[] amounts) {
            if (amounts == null || amounts.Length == 0) { Debug.LogError("Amounts data have not been assigned"); return; }
            _frontTable.SetActive(true);

            _useOneCurrency = true;
            _amounts = amounts;
            List<Sprite> sprites = new();

            for (int i = 0; i < _rewardElements.Length; i++) {
                if (i < amounts.Length) {
                    _rewardElements[i].SetAmount(_amounts[i]);
                    _rewardElements[i].SetSprites(_sprites[i]);
                    _rewardElements[i].SetSpriteAwards(_coinIcon.Sprite);

                    _rewardElements[i].Show();

                    sprites.Add(_sprites[i]);
                }
                else {
                    _rewardElements[i].Hide();
                }
            }

            foreach (var wheel in _wheels)
                wheel.Init(sprites);

            Init();
        }

        private void Init() {
            _uiParticleSystem = _confetiEffect.GetComponent<UIParticleSystem>();
        }

        public void SetPaidStart(int price) {
            _paidStartElement.SetPrice(price);
        }
        public void SetCoinIcon(CategoryIcon icon) {
            _paidStartElement.SetIcon(icon.Sprite);
            _coinIcon = icon;
        }

        public void SetChanceRatio(int value) {
            _chanceRatio = Mathf.Clamp(value, 0, 100);
        }

        public void TrySpin() {
            if (_paidStartElement.Price > 0 && _gateway.TryBuy(_paidStartElement.Price) == false)
                _panelLaunchFromAd.Show(Spin);
            else
                Spin();
        }

        public void Spin() {
            EnabledPullButtons(false);

            SpinSlots(CheckGivingReward());
        }

        private bool CheckGivingReward() {
            int randomChange = Random.Range(0, 100);

            if (randomChange < _chanceRatio)
                return true;
            else
                return false;
        }

        private void SpinSlots(bool giveReward) {
            foreach (var wheel in _wheels)
                wheel.StartRotating();

            StartCoroutine(StoppingRoulette(giveReward));
        }

        private IEnumerator StoppingRoulette(bool giveReward) {
            yield return new WaitForSeconds(_delayToStop);

            int slotsCount = _useOneCurrency ? _amounts.Length : _rewards.Count;
            int[] indexes = new int[3];

            if (giveReward) {
                _currentRewardIndex = Random.Range(0, slotsCount);
                for (int i = 0; i < indexes.Length; i++)
                    indexes[i] = _currentRewardIndex;
            }
            else {
                for (int i = 0; i < indexes.Length; i++) {
                    indexes[i] = Random.Range(0, slotsCount);

                    if (i == 2) // If the random numbers match, we regenerate the last one
                        while (indexes[0] == indexes[1] && indexes[1] == indexes[2])
                            indexes[i] = Random.Range(0, slotsCount);
                }
            }

            for (int i = 0; i < _wheels.Length; i++) {
                _wheels[i].StopAtIndex(indexes[i]);

                yield return new WaitForSeconds(_delayBetweenWheels);
            }

            while (true) {
                bool rotates = true;
                foreach (var wheel in _wheels) {
                    if (wheel.Rotates == false) {
                        rotates = false;
                        break;
                    }
                }

                if (rotates)
                    yield return new WaitForFixedUpdate();
                else
                    break;
            }

            if (giveReward) {
                if (_useOneCurrency) {
                    _popupPanel.Show(_coinIcon.Sprite, _amounts[_currentRewardIndex]);
                }
                else {
                    _rewards[_currentRewardIndex].ResetCurrentAmount();
                    _popupPanel.Show(_rewards[_currentRewardIndex].Icons.Sprite, _rewards[_currentRewardIndex].CurrentAmount);
                }
            }
            else {
                EnabledPullButtons(true);
            }
        }

        private void EnabledPullButtons(bool enabled) {
            _pullButton.Interactable = enabled;
            _pullHandle.Interactable = enabled;
        }

        public void ClaimRewardAd() {
            System.Action<bool> callBack = (success) => {
                if (success)
                    GetReward(2);
            };

            _gateway.StartRewardAD(callBack);
        }

        public void ClaimReward() {
            GetReward(1);
        }

        private void GetReward(int multiplier = 1) {
            if (multiplier < 1) { Debug.LogWarning($"The reward multiplier has been changed from {multiplier} to {1}, as it cannot be less than {1}"); multiplier = 1; }

            if (_useOneCurrency) {
                _gateway.AcceptAward(_amounts[_currentRewardIndex] * multiplier);
                _uiParticleSystem.material.mainTexture = _coinIcon.Texture;
            }
            else {
                _gateway.AcceptAward(_rewards[_currentRewardIndex].Type, _rewards[_currentRewardIndex].CurrentAmount * multiplier);
                _uiParticleSystem.material.mainTexture = _rewards[_currentRewardIndex].Icons.Texture;
            }

            _confetiEffect.Stop();
            _confetiEffect.Play();

            _popupPanel.Hide();
            EnabledPullButtons(true);
        }

        public void ResrtGame() {
            StopAllCoroutines();
            EnabledPullButtons(true);
        }
    }
}
