using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI.Extensions;
using UnityEngine;
using MiniGames;

namespace DropGame
{
    public class GameController : MonoBehaviour
    {
        [Header("Rewards Settings")]
        [Space]
        [SerializeField] private RewardElement[] _rewardElements;

        [Header("Game Inputs")]
        [Space]
        [SerializeField] private Gateway _gateway;
        [SerializeField] private CoinObject _coinObject;
        [SerializeField] private CategoryIcon _coinIcon;
        [Space]
        [SerializeField] private float coinSpeed = 5;
        [SerializeField] private int _horizontalLimit;

        [Header("UI Elements")]
        [Space]
        [SerializeField] private PopupPanel _popupPanel;
        [SerializeField] private CustomButton _dropButton;
        [SerializeField] private PaidStartElement _paidStartElement;
        [SerializeField] private PanelLaunchFromAd _panelLaunchFromAd;
        [SerializeField] private AudioSource _audioSelectRewardElement;

        [Header("Effect Settings")]
        [Space]
        [SerializeField] private ParticleSystem _confetiEffect;
        private UIParticleSystem _uiParticleSystem;

        private bool _isDrop;

        private List<Reward> _rewards = new();
        private int[] _amounts;
        private bool _useOneCurrency;
        private string _moveDirection = "Left";  //Go Left side first at start

        private Vector3 _coinStartPosition = new Vector3(0, 780, 0);


        /// <param name="set">List of rewards</param>
        /// <param name="randomOrder">Rewards are placed in a random order</param>
        /// <param name="randomSelection">Rewards can be repeated</param>
        public void Init(RewardSet set, bool randomOrder = false, bool randomSelection = false) {
            Init(set.Rewards, randomOrder, randomSelection);
        }

        /// <param name="rewards">List of rewards</param>
        /// <param name="randomOrder">Rewards are placed in a random order</param>
        /// <param name="randomSelection">Rewards can be repeated</param>
        public void Init(List<Reward> rewards = null, bool randomOrder = false, bool randomSelection = false) {
            if (rewards == null || rewards.Count == 0) { Debug.LogError("Rewards data have not been assigned"); return; }
            _useOneCurrency = false;

            List<Reward> rewardsContainer = new();
            for (int i = 0; i < _rewardElements.Length; i++) {
                if (randomSelection) {
                    Reward reward = rewards[Random.Range(0, rewards.Count)];
                    reward.ResetCurrentAmount();
                    _rewards.Add(reward.Clone());
                }
                else {
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
            }

            Init();
        }

        /// <param name="amounts">List of rewards</param>
        /// <param name="randomOrder">Rewards are placed in a random order</param>
        /// <param name="randomSelection">Rewards can be repeated</param>
        public void Init(int[] amounts, bool randomOrder = false, bool randomSelection = false) {
            if (amounts == null || amounts.Length == 0) { Debug.LogError("Amounts data have not been assigned"); return; }
            _useOneCurrency = true;
            _amounts = new int[_rewardElements.Length];

            List<int> amountsContainer = new();
            for (int i = 0; i < _rewardElements.Length; i++) {
                if (randomSelection) {
                    int index = amounts[Random.Range(0, amounts.Length)];
                    _amounts[i] = index;
                }
                else {
                    if (amountsContainer.Count == 0)
                        amountsContainer.AddRange(amounts);

                    int index = 0;
                    if (randomOrder)
                        index = Random.Range(0, amountsContainer.Count);

                    _amounts[i] = amountsContainer[index];
                    amountsContainer.RemoveAt(index);
                }
            }

            for (int i = 0; i < _rewardElements.Length; i++)
                _rewardElements[i].Set(_coinIcon.Sprite, _amounts[i]);

            Init();
        }

        private void Init() {
            _uiParticleSystem = _confetiEffect.GetComponent<UIParticleSystem>();
            _coinObject.OnResultObtained += ShowRewardResults;
            _coinObject.SetGravityScale(UnityEngine.Screen.height / 5.5f);
        }

        public void SetPaidStart(int price) {
            _paidStartElement.SetPrice(price);
        }
        public void SetCoinIcon(CategoryIcon icon) {
            _paidStartElement.SetIcon(icon.Sprite);
            _coinIcon = icon;
        }

        public void TryDropCoin() {
            if (_paidStartElement.Price > 0 && _gateway.TryBuy(_paidStartElement.Price) == false)
                _panelLaunchFromAd.Show(DropCoin);
            else
                DropCoin();
        }

        public void DropCoin() {
            _dropButton.Interactable = false;
            _isDrop = true;

            _coinObject.Drop();
        }

        private IEnumerator MoveCoin() {
            _isDrop = false;

            while (!_isDrop) {
                if (_moveDirection == "Left") {
                    if (Mathf.RoundToInt(_coinObject.transform.localPosition.x) < _horizontalLimit) {
                        _coinObject.transform.localPosition += new Vector3(coinSpeed * Time.deltaTime * 100, 0, 0);
                    }
                    else
                        _moveDirection = "Right";
                }
                else if (_moveDirection == "Right") {
                    if (Mathf.RoundToInt(_coinObject.transform.localPosition.x) > -_horizontalLimit) {
                        _coinObject.transform.localPosition += new Vector3(-coinSpeed * Time.deltaTime * 100, 0, 0);
                    }
                    else
                        _moveDirection = "Left";
                }
                yield return null;
            }
        }

        public void ShowRewardResults() {
            _rewardElements[_coinObject.RewardIndex].PlayCollect();
            _audioSelectRewardElement.Play();

            Invoke(nameof(ShowRewardPanel), 2);
        }
        private void ShowRewardPanel() {
            if (_useOneCurrency)
                _popupPanel.Show(_coinIcon.Sprite, _amounts[_coinObject.RewardIndex]);
            else
                _popupPanel.Show(_rewards[_coinObject.RewardIndex].Icons.Sprite, _rewards[_coinObject.RewardIndex].CurrentAmount);
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
                _gateway.AcceptAward(_amounts[_coinObject.RewardIndex] * multiplier);
                _uiParticleSystem.material.mainTexture = _coinIcon.Texture;
            }
            else {
                _gateway.AcceptAward(_rewards[_coinObject.RewardIndex].Type, _rewards[_coinObject.RewardIndex].CurrentAmount * multiplier);
                _uiParticleSystem.material.mainTexture = _rewards[_coinObject.RewardIndex].Icons.Texture;
            }

            _confetiEffect.Stop();
            _confetiEffect.Play();

            _popupPanel.Hide();
            ResetGame();
        }

        public void ResetGame() {
            _isDrop = false;
            _coinObject.Reset();
            _coinObject.transform.localPosition = _coinStartPosition;

            if (_useOneCurrency == false) {
                for (int i = 0; i < _rewardElements.Length; i++) {
                    _rewards[i].ResetCurrentAmount();
                    _rewardElements[i].Set(_rewards[i].Icons.Sprite, _rewards[i].CurrentAmount);
                }
            }

            StopAllCoroutines();
            StartCoroutine(MoveCoin());
            _dropButton.Interactable = true;
            _rewardElements[_coinObject.RewardIndex].Reset();
            _popupPanel.Hide();
        }
    }
}
