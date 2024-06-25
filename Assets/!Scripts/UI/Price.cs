using Extension;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Price : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void Show(int price)
        {
            gameObject.Activate();
            _text.text = price.ToString();
        }

        public void Show(string max)
        {
            _text.text = max;
        }

        public void Hide() => gameObject.Deactivate();
    }
}