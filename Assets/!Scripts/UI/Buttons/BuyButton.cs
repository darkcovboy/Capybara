using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class BuyButton : MonoBehaviour
    {
        public event Action Click;

        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _lockAnimationDuration = 0.4f;
        [SerializeField] private float _lockAnimationStrength = 2f;

        private bool _isLock;
        private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);
    
        public void UpdateText(int price) => _text.text = price.ToString();

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
            if(_isLock)
            {
                transform.DOShakePosition(_lockAnimationDuration, _lockAnimationStrength);
            }

            Click?.Invoke();
        }
    }
}