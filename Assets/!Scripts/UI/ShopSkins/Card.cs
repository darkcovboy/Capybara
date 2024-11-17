using TMPro;
using UnityEngine;

namespace UI.ShopSkins
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Transform _skinPlacement;
        [SerializeField] private TextMeshProUGUI _text;

        public Transform SkinPlacement => _skinPlacement;

        public void UpdateText(string text)
        {
            _text.text = text;
        }
    }
}