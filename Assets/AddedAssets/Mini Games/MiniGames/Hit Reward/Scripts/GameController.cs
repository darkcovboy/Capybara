using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI.Extensions;
using UnityEngine;
using MiniGames;

namespace HitRewardSpace
{
    public class GameController : MonoBehaviour
    {
        [Header("Rewards Settings")]
        [Space]
        [SerializeField] private List<PieceFeatures> _piecesOfWheel;
        [SerializeField] private Transform _pieceContainer;
        [SerializeField] private PieceObject _piecePrefab;

        [Header("Other")]
        [Space]
        [SerializeField] private Gateway _gateway;
        [SerializeField] private Animator _animaor;
        [SerializeField] private CategoryIcon _coinIcon;
        [Space]
        [SerializeField] private float _slowdownsTime = 2.5f;
        [SerializeField] private Vector2 _rotationSpeed = new Vector2(330, 360);
        private float _currentSpeed;

        [Header("UI Elements")]
        [Space]
        [SerializeField] private PopupPanel _popupPanel;
        [SerializeField] private CustomButton _hitButton;
        [SerializeField] private PaidStartElement _paidStartElement;
        [SerializeField] private PanelLaunchFromAd _panelLaunchFromAd;

        [Header("Knife")]
        [Space]
        [SerializeField] private float _knifeSpeed = 8f;
        [SerializeField] private Transform _knifeContainer;
        [SerializeField] private Knife[] _knifePrefabs;
        private Knife _knife;

        [Header("Audio")]
        [Space]
        [SerializeField] private AudioSource _audioKnife;
        [SerializeField] private AudioClip _clipStartKnife;
        [SerializeField] private AudioClip _clipEndKnife;

        [Header("Effect Settings")]
        [Space]
        [SerializeField] private ParticleSystem _confetiEffect;
        private UIParticleSystem _uiParticleSystem;

        private Coroutine _rotation;
        private float _angleOnePiece;
        private float _anglsePiecehalfTurn;

        private bool _isInit;
        private List<Reward> _rewards = new();
        private int[] _amounts;
        private bool _useOneCurrency;

        private float WheelLocalAngl {
            get {
                int integer = (int)Mathf.Abs(_pieceContainer.rotation.eulerAngles.z + 180) / 360;
                float angle = Mathf.Abs(_pieceContainer.rotation.eulerAngles.z + 180) - integer * 360;
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

            CreateWheel();

            _uiParticleSystem = _confetiEffect.GetComponent<UIParticleSystem>();

            _isInit = true;
        }

        public void CreateWheel() {
            float startingAngle = -_anglsePiecehalfTurn;

            for (int i = 0; i < _piecesOfWheel.Count; i++) {
                PieceObject pieceObj = Instantiate(_piecePrefab, Vector3.zero, new Quaternion(0, 0, 0, 0), _pieceContainer.transform);

                pieceObj.transform.name = "Piece " + i;
                pieceObj.transform.localPosition = Vector3.zero;
                pieceObj.transform.Rotate(0, 0, startingAngle);

                if (_useOneCurrency)
                    pieceObj.SetValues(_piecesOfWheel[i], _coinIcon.Sprite, _amounts[i]);
                else
                    pieceObj.SetValues(_piecesOfWheel[i], _rewards[i]);

                startingAngle -= _angleOnePiece;
            }

            if (_knife)
                Destroy(_knife.gameObject);

            _knife = Instantiate(_knifePrefabs[Random.Range(0, _knifePrefabs.Length)], _knifeContainer);
            _knife.OnHit += PlayHitAnim;

            ResetKnife();
        }

        public void SetPaidStartAmount(int price) {
            _paidStartElement.SetPrice(price);
        }
        public void SetCoinIcon(CategoryIcon icon) {
            _paidStartElement.SetIcon(icon.Sprite);
            _coinIcon = icon;
        }

        private void ResetKnife() {
            _knife.transform.SetParent(_knifeContainer);
            _knife.transform.localPosition = Vector3.zero;
            _knife.transform.rotation = Quaternion.identity;
        }

        private IEnumerator TurnWheel() {
            _currentSpeed = Random.Range(_rotationSpeed.x, _rotationSpeed.y);

            while (true) {
                _pieceContainer.transform.Rotate(Vector3.forward * _currentSpeed * Time.fixedDeltaTime);

                yield return new WaitForFixedUpdate();
            }
        }

        public void TryStartKnife() {
            if (_paidStartElement.Price > 0 && _gateway.TryBuy(_paidStartElement.Price) == false)
                _panelLaunchFromAd.Show(StartKnife);
            else
                StartKnife();
        }

        public void StartKnife() {
            if (_isInit == false) { Debug.LogError("It is not possible to start the knife until the wheel is initialized"); return; }

            _knife.SetSpeed(Vector2.Distance(_knifeContainer.position, _pieceContainer.position) * _knifeSpeed);
            _hitButton.Interactable = false;
            _knife.Play();

            _audioKnife.clip = _clipStartKnife;
            _audioKnife.Play();
        }

        public void PlayHitAnim() {
            _animaor.SetBool("isKnifeHit", true);

            _audioKnife.clip = _clipEndKnife;
            _audioKnife.Play();

            StartCoroutine(Stopping());
        }

        private IEnumerator Stopping() {
            Quaternion targetRot = _pieceContainer.rotation;

            float startSpeed = _currentSpeed;
            for (float t = 0; t < 1; t += Time.fixedDeltaTime / _slowdownsTime) {
                _currentSpeed = Mathf.Lerp(startSpeed, 0, t);

                yield return new WaitForFixedUpdate();
            }

            WheelStop();

            Quaternion currentRot = _pieceContainer.rotation;
            for (float t = 0; t < 1; t += Time.fixedDeltaTime) {
                _pieceContainer.rotation = Quaternion.Lerp(currentRot, targetRot, func(t));

                yield return new WaitForFixedUpdate();
            }
            _pieceContainer.rotation = targetRot;

            _animaor.SetBool("isKnifeHit", false);

            int rewardIndex = CurrentRewardIndex;
            if (_useOneCurrency)
                _popupPanel.Show(_coinIcon.Sprite, _amounts[rewardIndex]);
            else
                _popupPanel.Show(_rewards[rewardIndex].Icons.Sprite, _rewards[rewardIndex].CurrentAmount);

            // easy in easy out
            float func(float x) {
                float sqt = x * x;
                return sqt / (2f * (sqt - x) + 1f);
            }
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

            ResetGame();
            WheelTurn();
        }

        public void ResetGame() {
            StopAllCoroutines();
            ResetKnife();
            _popupPanel.Hide();
            _hitButton.Interactable = true;
        }

        public void WheelTurn() {
            WheelStop();
            _rotation = StartCoroutine(TurnWheel());
        }

        public void WheelStop() {
            if (_rotation != null) {
                StopCoroutine(_rotation);
                _rotation = null;
            }
        }
    }
}
