using Extension;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AdPrice : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void Show(int current, int max)
        {
            gameObject.Activate();
            _text.text = $"{current}/{max}";
        }

        public void Show(string max)
        {
            _text.text = max;
        }

        public void Hide() => gameObject.Deactivate();
    }
}