using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class LoseScreen : Screen
    {
        public event Action OnReloadButtonPress;
        
        [SerializeField] private Button _reloadButton;

        private void OnEnable()
        {
            _reloadButton.onClick.AddListener(() => OnReloadButtonPress?.Invoke());
        }

        private void OnDisable()
        {
            _reloadButton.onClick.RemoveListener(() => OnReloadButtonPress?.Invoke());
        }
    }
}