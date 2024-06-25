using System;
using Extension;
using Player.Skins;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace UI.ShopSkins
{
    public class SkinView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<SkinView> Click;

        [SerializeField] private Sprite _standartBackground;
        [SerializeField] private Sprite _highlitedBackground;

        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _lockImage;

        [SerializeField] private Price _price;

        [SerializeField] private Image _selectionText;

        private Image _backgroundImage;

        public CharacterSkinItem CharacterSkinItem { get; private set; }
        public bool IsLock { get; private set; }
        public bool IsSelected { get; private set; }
        public int Price => CharacterSkinItem.Price;
        public ViewModel ViewModel => CharacterSkinItem.ViewModel;

        public void Initiliaze(CharacterSkinItem skinItem)
        {
            _backgroundImage = GetComponent<Image>();
            _backgroundImage.sprite = _standartBackground;

            CharacterSkinItem = skinItem;

            _contentImage.sprite = skinItem.Image;

            _price.Show(skinItem.Price);
        }

        public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

        public void Lock()
        {
            IsLock = true;
            _lockImage.gameObject.Activate();
            _price.Show(CharacterSkinItem.Price);
        }

        public void Unlock()
        {
            IsLock = false;
            _lockImage.gameObject.Deactivate();
            _price.Hide();
        }

        public void Select()
        {
            IsSelected = true;
            _selectionText.gameObject.Activate();
        }
    
        public void Unselect()
        {
            IsSelected = false;
            _selectionText.gameObject.Deactivate();
        }

        public void Highlight() => _backgroundImage.sprite = _highlitedBackground;

        public void UnHighlight() => _backgroundImage.sprite = _standartBackground;
    }
}