using System;
using Player.Counter;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.InfoShower
{
    public class MoneyShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private IMoneyCounter _moneyCounter;

        [Inject]
        public void Constructor(IMoneyCounter moneyCounter)
        {
            _moneyCounter = moneyCounter;
            _moneyCounter.OnMoneyChanged += UpdateText;
        }

        private void OnEnable()
        {
            UpdateText(_moneyCounter.Money);
        }
        
        private void OnDestroy()
        {
            _moneyCounter.OnMoneyChanged -= UpdateText;
        }

        private void UpdateText(int money)
        {
            _text.text = $"{money}";
        }
    }
}