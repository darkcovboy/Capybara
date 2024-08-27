using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGames
{
    public class PopupPanel : MonoBehaviour
    {
        [SerializeField] private Image _rewardImage;
        [SerializeField] private TextMeshProUGUI _rewardText;
        [SerializeField] private Transform _content;
        [SerializeField] private Transform _popup;
        [SerializeField] private float _timeAappearanceontent = 0.3f;
        [SerializeField] private ParticleSystem _confetiEffect;
        [SerializeField] private Image _background;
        [SerializeField] private AudioSource _audioOpen;


        public void Show(Sprite sprite, int amount) {
            _rewardImage.sprite = sprite;
            _rewardText.text = amount.ToString();

            gameObject.SetActive(true);
            _content.localScale = Vector3.zero;

            _audioOpen.Play();

            StartCoroutine(ShowPopup());
        }

        public void Hide() {
            StopAllCoroutines();
            gameObject.SetActive(false);
        }

        IEnumerator ShowPopup() {
            _confetiEffect.Play();

            Color bgColor = _background.color;
            float alpha = bgColor.a;
            for (float i = 0; i < 1; i += Time.deltaTime / _timeAappearanceontent) {
                _content.localScale = Vector3.one * i;

                bgColor.a = Mathf.Lerp(0, alpha, i);
                _background.color = bgColor;

                yield return null;
            }
            _content.localScale = Vector3.one;

            bgColor.a = alpha;
            _background.color = bgColor;
        }
    }
}
