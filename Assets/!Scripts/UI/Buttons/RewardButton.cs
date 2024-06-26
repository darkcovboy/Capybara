using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public abstract class RewardButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        protected abstract void HandleReward();

        private void OnButtonClick()
        {
            HandleReward();
            HideButton();
        }

        private void HideButton()
        {
            _button.interactable = false;
        }
    }
}