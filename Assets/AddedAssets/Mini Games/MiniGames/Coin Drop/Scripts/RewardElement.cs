using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DropGame
{
    public class RewardElement : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Animator _animator;


        public void Set(Sprite sprite, int amount) {
            _image.sprite = sprite;
            _text.text = amount.ToString();
        }

        public void PlayCollect() {
            _animator.SetTrigger("RewardFly");
        }

        public void Reset() {
            _animator.SetTrigger("Reset");
        }
    }
}