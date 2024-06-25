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
        [SerializeField] private Color _lockColor;
        [SerializeField] private Color _unlockColor;

        [SerializeField] private float _lockAnimationDuration = 0.4f;
        [SerializeField] private float _lockAnimationStrength = 2f;

        private bool _isLock;
        private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);
    
        public void UpdateText(int price) => _text.text = price.ToString();

        public void Lock()
        {
            _isLock = true;
            /*
            _text.color = _lockColor;
            _text.color.a = 255;
            */
        }

        public void Unlock()
        {
            _isLock = false;
            //_text.color = _unlockColor;
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