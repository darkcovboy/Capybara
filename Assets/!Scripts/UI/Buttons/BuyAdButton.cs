using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class BuyAdButton : MonoBehaviour
    {
        public event Action Click;

        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        private bool _isLock;
        private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);
    
        public void UpdateText(int current, int max) => _text.text = $"{current}/{max}";

        public void Lock()
        {
            _isLock = true;
        }

        public void Unlock()
        {
            _isLock = false;
        }

        private void OnButtonClick()
        {
            Click?.Invoke();
        }
    }
}