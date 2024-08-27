using System;
using System.Collections;
using UI.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class SettingsScreen : Screen
    {
        [Header("Toggles")]
        [SerializeField] private Toggle _soundOnToggle;
        [SerializeField] private Toggle _soundOffToggle;
        [SerializeField] private Button _closeButton;
        [SerializeField] private ScaleDownAnimation _scaleUpAnimation;

        private void OnEnable()
        {
            if (AudioListener.pause)
            {
                _soundOffToggle.isOn = true;
            }
            else
            {
                _soundOnToggle.isOn = true;
            }
            
            _soundOnToggle.onValueChanged.AddListener(delegate { ToggleSound(_soundOnToggle); });
            _soundOffToggle.onValueChanged.AddListener(delegate { ToggleSound(_soundOffToggle); });
            
            _closeButton.onClick.AddListener(() =>
            {
                StartCoroutine(Close());
            });
        }

        private void OnDisable()
        {
            _soundOnToggle.onValueChanged.RemoveListener(delegate { ToggleSound(_soundOnToggle); });
            _soundOffToggle.onValueChanged.RemoveListener(delegate { ToggleSound(_soundOffToggle); });
            
            _closeButton.onClick.RemoveListener(() =>
            {
                StartCoroutine(Close());
            });
        }

        private IEnumerator Close()
        {
            _scaleUpAnimation.ScaleDown();
            yield return new WaitForSeconds(_scaleUpAnimation.Duration);
            gameObject.SetActive(false);
        }

        private void ToggleSound(Toggle changedToggle)
        {
            if (changedToggle == _soundOnToggle && changedToggle.isOn)
            {
                AudioListener.pause = false;
            }
            else if (changedToggle == _soundOffToggle && changedToggle.isOn)
            {
                AudioListener.pause = true;
            }
        }
    }
}