using System;
using Extension;
using Player.Skins;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace UI.ShopSkins
{
    public class SkinView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<SkinView> Click;

        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Color _standartBackground;
        [SerializeField] private Color _highlitedBackground;

        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _lockImage;

        [SerializeField] private Price _price;
        [SerializeField] private AdPrice _adPrice;

        [SerializeField] private TMP_Text _selectionText;


        public CharacterSkinItem CharacterSkinItem { get; private set; }
        public bool IsLock { get; private set; }
        public bool IsSelected { get; private set; }
        public int Price => CharacterSkinItem.Price;
        public ViewModel ViewModel => CharacterSkinItem.ViewModel;
        public int Stars => CharacterSkinItem.Stars;
        public string ModelName => CharacterSkinItem.ModelName;

        public void Initiliaze(CharacterSkinItem skinItem)
        {
            _backgroundImage.color = _standartBackground;

            CharacterSkinItem = skinItem;

            _contentImage.sprite = skinItem.Image;

            DefinePrice();
        }

        public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

        public void Lock()
        {
            IsLock = true;
            _lockImage.gameObject.Activate();
            DefinePrice();
        }

        public void Unlock()
        {
            IsLock = false;
            _lockImage.gameObject.Deactivate();
            HidePrices();
        }

        public void Select()
        {
            IsSelected = true;
            _selectionText.gameObject.Activate();
            HidePrices();
        }

        private void HidePrices()
        {
            _price.Hide();
            _adPrice.Hide();
        }

        public void Unselect()
        {
            IsSelected = false;
            _selectionText.gameObject.Deactivate();
        }

        private void DefinePrice()
        {
            if (CharacterSkinItem.IsBuingForAds)
            {
                _price.Hide();
                _adPrice.Show(0, CharacterSkinItem.AmountOfAds);
            }
            else
            {
                _adPrice.Hide();
                _price.Show(CharacterSkinItem.Price);
            }
        }

        public void Highlight() => _backgroundImage.color = _highlitedBackground;

        public void UnHighlight() => _backgroundImage.color = _standartBackground;
    }
}