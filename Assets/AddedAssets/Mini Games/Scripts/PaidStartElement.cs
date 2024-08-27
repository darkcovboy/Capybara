using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGames
{
    public class PaidStartElement : MonoBehaviour
    {
        public int Price { get; private set; }

        [SerializeField] private TextMeshProUGUI _paidStartText;
        [SerializeField] private Image _coinImage;


        public void SetPrice(int price) {
            Price = price;
            _paidStartText.text = price.ToString();

            if (price > 0)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }

        public void SetIcon(Sprite coinSprite) {
            _coinImage.sprite = coinSprite;
        }
    }
}