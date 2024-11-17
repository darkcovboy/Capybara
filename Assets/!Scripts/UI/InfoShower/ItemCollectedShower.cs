using System;
using Items.Collector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.InfoShower
{
    public class ItemCollectedShower : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;
        
        private IItemCollected _itemCollected;

        [Inject]
        public void Constructor(IItemCollected itemCollected)
        {
            _itemCollected = itemCollected;
            _itemCollected.OnItemCollected += UpdateText;
        }

        private void OnEnable()
        {
            UpdateText(0, _itemCollected.MAXItems);
        }

        private void UpdateText(int current, int max)
        {
            _text.text = $"{current}";
            if (current == 0)
            {
                _image.fillAmount = 0;
            }
            else
            {
                _image.fillAmount = (float)current / max;
            }
        }
    }
}