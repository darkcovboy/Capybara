using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace UI.Screens
{
    public class RewardArrowMovement : MonoBehaviour
    {
        [SerializeField, InfoBox("Время на один круг")] private float _timeToCompleteCircle = 1.2f;
        [SerializeField] private float _minRotation = -90f;
        [SerializeField] private float _maxRotation = 90f;

        private float _currentRotation = 0f; // Текущее положение
        private int _direction = 1; // Направление движения, 1 - вправо, -1 - влево
        private float _timeLeft = 0f; // Время до окончания движения

        private float _multiplyFactor = 1.5f;

        [SerializeField] private TextMeshProUGUI _finalCoinsText;
        [SerializeField] private TextMeshProUGUI _advCoinsText;
        [SerializeField] private int _finalCoins;

        public float MultiplyFactor => _multiplyFactor;

        private void Start()
        {
            _finalCoins = int.Parse(_finalCoinsText.text);
        }

        private void Update()
        {
            if (_timeLeft <= 0f) // Если прошел указанный промежуток времени, меняем направление
            {
                _direction *= -1;
                _timeLeft = _timeToCompleteCircle;
            }

            float amount = (_timeToCompleteCircle - _timeLeft) / _timeToCompleteCircle; // Рассчитываем значение между minRotation и maxRotation
            _currentRotation = Mathf.Lerp(_minRotation, _maxRotation, amount); // Получаем текущее положение
            float newRotation = _currentRotation * _direction;
            transform.rotation = Quaternion.Euler(0f, 0f, newRotation); // Применяем текущее положение

            if (newRotation <= 90 && newRotation >= 35)
            {
                _multiplyFactor = 1.5f;
            }
            else if (newRotation < 35 && newRotation >= -40)
            {
                _multiplyFactor = 2f;
            }
            else if (newRotation < -40 && newRotation >= -70)
            {
                _multiplyFactor = 2.5f;
            }
            else if (newRotation < -70 && newRotation >= -90)
            {
                _multiplyFactor = 3f;
            }
        
            _timeLeft -= Time.deltaTime; // Уменьшаем оставшееся время
            _advCoinsText.text = ((int)(_finalCoins * _multiplyFactor)).ToString();
        }
    }
}