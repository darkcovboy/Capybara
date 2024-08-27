using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniGames;
using TMPro;
using UnityEngine.UI.Extensions;

namespace FortuneWheel
{
    public class GameController : MonoBehaviour
    {
        [Header("Rewards Settings")]
        [Space]
        [SerializeField] private List<PieceFeatures> _piecesOfWheel;
        [Space]
        [SerializeField] private Transform _pieceContainer;
        [SerializeField] private PieceObject _piecePrefab;

        [Header("Other")]
        [Space]
        [SerializeField] private Gateway _gateway;
        [SerializeField] private CategoryIcon _coinIcon;
        [Space(5)]
        [SerializeField] private float _rotationTime = 5;

        [Header("UI Elements")]
        [Space]
        [SerializeField] private PopupPanel _popupPanel;
        [SerializeField] private Button _turnButton;
        [SerializeField] private Stopper _stopper;
        [SerializeField] private PaidStartElement _paidStartElement;
        [SerializeField] private PanelLaunchFromAd _panelLaunchFromAd;

        [Header("Header")]
        [Space]
        [SerializeField] private Image _rewardImageHeader;
        [SerializeField] private TextMeshProUGUI _rewardTextHeader;

        [Header("Effect Settings")]
        [Space]
        [SerializeField] private ParticleSystem _confetiEffect;
        private UIParticleSystem _uiParticleSystem;

        [Header("Audio")]
        [Space]
        [SerializeField] private AudioSource _audioTick;

        private float _wheelAngle;
        private float _angleOnePiece;
        private float _anglsePiecehalfTurn;
        private bool _useOneCurrency;
        private int[] _amounts;
        private List<Reward> _rewards = new();

        public bool IsInit { get; private set; }

        private float WheelLocalAngl {
            get {
                int integer = (int)Mathf.Abs(_wheelAngle) / 360;
                float angle = Mathf.Abs(_wheelAngle) - integer * 360;
                return angle;
            }
        }

        private int CurrentRewardIndex {
            get {
                return Mathf.CeilToInt(WheelLocalAngl / _angleOnePiece) - 1;
            }
        }


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
            for (int i = 0; i < _piecesOfWheel.Count; i++) {
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
            _amounts = new int[_piecesOfWheel.Count];

            List<int> amountsContainer = new();
            for (int i = 0; i < _piecesOfWheel.Count; i++) {
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

            Init();
        }

        private void Init() {
            for (int i = _pieceContainer.childCount; i > 0; i--)
                Destroy(_pieceContainer.GetChild(i - 1).gameObject);

            _angleOnePiece = 360 / _piecesOfWheel.Count;
            _anglsePiecehalfTurn = _angleOnePiece / 2;
            _wheelAngle = -_anglsePiecehalfTurn;

            _pieceContainer.rotation = Quaternion.Euler(Vector3.forward * _wheelAngle);

            CreateWheel();

            _uiParticleSystem = _confetiEffect.GetComponent<UIParticleSystem>();

            IsInit = true;
        }

        public void CreateWheel() {
            float startingAngle = 0;

            for (int i = 0; i < _piecesOfWheel.Count; i++) {
                PieceObject pieceObj = Instantiate(_piecePrefab, Vector3.zero, new Quaternion(0, 0, 0, 0), _pieceContainer.transform);

                pieceObj.transform.name = "Piece " + i;
                pieceObj.transform.localPosition = Vector3.zero;
                pieceObj.transform.Rotate(0, 0, Mathf.Abs(startingAngle) + _anglsePiecehalfTurn);

                if (_useOneCurrency)
                    pieceObj.SetValues(_piecesOfWheel[i], _coinIcon.Sprite, _amounts[i]);
                else
                    pieceObj.SetValues(_piecesOfWheel[i], _rewards[i]);

                startingAngle += _angleOnePiece;
            }

            SetHeader(CurrentRewardIndex);
        }

        public void SetPaidStart(int price) {
            _paidStartElement.SetPrice(price);
        }
        public void SetCoinIcon(CategoryIcon icon) {
            _paidStartElement.SetIcon(icon.Sprite);
            _coinIcon = icon;
        }

        public void TryTurnWheel() {
            if (_paidStartElement.Price > 0 && _gateway.TryBuy(_paidStartElement.Price) == false)
                _panelLaunchFromAd.Show(TurnWheel);
            else
                TurnWheel();
        }

        public void TurnWheel() {
            if (IsInit == false) { Debug.LogError("It is impossible to start the wheel until it is initialized"); return; }

            _turnButton.interactable = false;

            StartCoroutine(TurnRoutine(Random.Range(0, _piecesOfWheel.Count) * _angleOnePiece, Random.Range(5, 8)));
        }

        private IEnumerator TurnRoutine(float finalAngle, int fullCircles) {
            float offset = Random.Range(-_anglsePiecehalfTurn / 2, _anglsePiecehalfTurn / 2);

            float startAngle = _wheelAngle;
            float targetAngle = _wheelAngle - (fullCircles * 360 + finalAngle);
            int index = CurrentRewardIndex;

            for (float time = 0; time < _rotationTime; time += Time.deltaTime) {
                float t = time / _rotationTime;

                t = 1f - (1f - t) * (1f - t);
                //t = t * t * t * (t * (6f * t - 15f) + 10f);

                int currentIndex = CurrentRewardIndex;
                if (index != currentIndex) {
                    index = currentIndex;

                    SetHeader(currentIndex);
                    _stopper.Play();
                    _audioTick.Play();
                }

                _wheelAngle = Mathf.Lerp(startAngle, targetAngle + offset, t);
                _pieceContainer.transform.eulerAngles = new Vector3(0, 0, _wheelAngle);

                yield return null;
            }

            startAngle = targetAngle + offset;
            for (float time = 0; time < 1; time += Time.deltaTime / 0.5f) {
                _wheelAngle = Mathf.Lerp(startAngle, targetAngle, time);
                _pieceContainer.transform.eulerAngles = new Vector3(0, 0, _wheelAngle);

                yield return null;
            }
            _wheelAngle = targetAngle;
            _pieceContainer.transform.eulerAngles = new Vector3(0, 0, _wheelAngle);

            int rewardIndex = CurrentRewardIndex;
            if (_useOneCurrency)
                _popupPanel.Show(_coinIcon.Sprite, _amounts[rewardIndex]);
            else
                _popupPanel.Show(_rewards[rewardIndex].Icons.Sprite, _rewards[rewardIndex].CurrentAmount);
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
                _gateway.AcceptAward(_amounts[CurrentRewardIndex] * multiplier);
                _uiParticleSystem.material.mainTexture = _coinIcon.Texture;
            }
            else {
                _gateway.AcceptAward(_rewards[CurrentRewardIndex].Type, _rewards[CurrentRewardIndex].CurrentAmount * multiplier);
                _uiParticleSystem.material.mainTexture = _rewards[CurrentRewardIndex].Icons.Texture;
            }

            _confetiEffect.Stop();
            _confetiEffect.Play();

            _popupPanel.Hide();
            _turnButton.interactable = true;
        }

        private void SetHeader(int index) {
            if (_useOneCurrency) {
                _rewardImageHeader.sprite = _coinIcon.Sprite;
                _rewardTextHeader.text = _amounts[index].ToString();
            }
            else {
                _rewardImageHeader.sprite = _rewards[index].Icons.Sprite;
                _rewardTextHeader.text = _rewards[index].CurrentAmount.ToString();
            }
        }

        public void ResetGame() {
            StopAllCoroutines();

            _wheelAngle = -_anglsePiecehalfTurn;
            _pieceContainer.rotation = Quaternion.Euler(Vector3.forward * _wheelAngle);

            _turnButton.interactable = true;
            _popupPanel.Hide();
        }
    }
}
