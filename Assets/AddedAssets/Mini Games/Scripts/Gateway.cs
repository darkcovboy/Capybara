using UnityEngine;

namespace MiniGames
{
    public class Gateway : MonoBehaviour
    {
        [SerializeField] private MiniGame _miniGame;


        private void Awake() {
            _miniGame.Init();
        }

        private void Start() {
            Hide();
        }

        public void Open() {
            _miniGame.Open();
        }

        public void Hide() {
            _miniGame.Hide();
        }

        public void StartRewardAD(System.Action<bool> callBack) {
            Debug.LogWarning("Instead of this challenge, you need to run an advertisement!");
            callBack.Invoke(true);
        }

        public void AcceptAward(RewardEnum type, int count) {
            Debug.LogWarning($"A reward has been received: {type} {count}");
        }
        public void AcceptAward(int count) {
            Debug.LogWarning($"A reward has been received: {count}");
        }

        public bool TryBuy(int count) {
            Debug.LogWarning($"If the player has money, then make a purchase");
            return true;
        }

        public static void SetLanguage(Language language) {
            LocalisationManager.SetLanguage(language);
        }
    }
}
