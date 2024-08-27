using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniGames
{
    [RequireComponent(typeof(Image))]
    public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private bool _isInteractable = true;

        [Header("Image")]
        [Space]
        [SerializeField] private Sprite _pressedSprite;
        [SerializeField] private Sprite _disabledSprite;
        private Sprite _defaultSprite;
        private Image _image;

        [Header("Audio")]
        [Space]
        [SerializeField] private AudioSource _audio;
        [SerializeField] private AudioClip _downClip;
        [SerializeField] private AudioClip _upClip;

        private bool _isPointerOver;
        private bool _isPress;

        [Header("Events")]
        [Space]
        public UnityEvent OnDown;
        public UnityEvent OnUp;


        private void Awake() {
            _image = GetComponent<Image>();
            _defaultSprite = _image.sprite;

            if (_disabledSprite && _isInteractable == false)
                _image.sprite = _disabledSprite;
        }

        public bool Interactable {
            get {
                return _isInteractable;
            }
            set {
                _isInteractable = value;

                if (_disabledSprite) {
                    if (value)
                        _image.sprite = _defaultSprite;
                    else
                        _image.sprite = _disabledSprite;
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData) {
            if (Interactable == false) return;

            if (_pressedSprite)
                _image.sprite = _pressedSprite;

            if (_audio) {
                if (_downClip)
                    _audio.clip = _downClip;

                _audio.Play();
            }

            _isPress = true;

            OnDown.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData) {
            if (_isPress == false) return;

            _image.sprite = _defaultSprite;

            if (_audio) {
                if (_upClip)
                    _audio.clip = _upClip;

                _audio.Play();
            }

            _isPress = false;

            if (_isPointerOver)
                OnUp.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (Interactable == false) return;

            _isPointerOver = true;
        }

        public void OnPointerExit(PointerEventData eventData) {
            _isPointerOver = false;
        }
    }
}