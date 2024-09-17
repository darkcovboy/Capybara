using System.Linq;
using DefaultNamespace;
using Extension;
using Player.Counter;
using SaveSystem;
using Sirenix.OdinInspector;
using UI.Buttons;
using UI.Screens;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Screen = UI.Screens.Screen;

namespace UI.ShopSkins
{
    public class Shop : Screen
    {
         [SerializeField] private ShopContent _contentItems;
         [SerializeField] private BuyButton _buyButton;
    [SerializeField] private Button _selectionButton;
    [SerializeField] private BuyAdButton _buyForAdButton;
    [SerializeField] private Image _selectedText;

    [SerializeField] private Button _exitButton;

    [SerializeField] private SkinPlacement _skinPlacement;

    [SerializeField] private ShopPanel _shopPanel;

    private SkinView _previousSkinView;

    private MoneyCounter _moneyCounter;
    private SaveManager _saveManager;

    private void OnEnable()
    {
        _shopPanel.Show(_contentItems.CharacterSkins.Cast<CharacterSkinItem>());
        _shopPanel.SkinViewClicked += OnItemViewClicked;
        _buyButton.Click += OnBuyButtonClick;
        _buyForAdButton.Click += OnAdButtonClick;
        _selectionButton.onClick.AddListener(OnSelectionButtonClick);
        _exitButton.onClick.AddListener(CloseShop);
    }

    private void OnDisable()
    {
        _shopPanel.SkinViewClicked -= OnItemViewClicked;
        _buyButton.Click -= OnBuyButtonClick;
        _buyForAdButton.Click -= OnAdButtonClick;
        _selectionButton.onClick.RemoveListener(OnSelectionButtonClick);
        _exitButton.onClick.RemoveListener(CloseShop);
    }

    [Inject]
    public void Constructor(MoneyCounter moneyCounter, SaveManager saveManager)
    {
        _moneyCounter = moneyCounter;
        _saveManager = saveManager;
    }

    private void OnItemViewClicked(SkinView item)
    {
        _previousSkinView = item;
        _skinPlacement.InstantiateModel(item.ViewModel);

        if(!item.IsLock)
        {
            if(item.IsSelected)
            {
                ShowSelectedText();
                return;
            }

            ShowSelectionButton();
        }
        else
        {
            if (!_saveManager.PlayerData.AdViewsForSkins.ContainsKey(item.CharacterSkinItem.CharacterType))
            {
                _saveManager.PlayerData.AdViewsForSkins[item.CharacterSkinItem.CharacterType] = 0;
            }

            int adViews = _saveManager.PlayerData.AdViewsForSkins[item.CharacterSkinItem.CharacterType];

            if(item.CharacterSkinItem.IsBuingForAds)
                ShowAdButton(adViews, item.CharacterSkinItem.AmountOfAds);
            else
                ShowBuyButton(item.Price);
        }
    }

    private void OnBuyButtonClick()
    {
        if(_moneyCounter.TrySpendMoney(_previousSkinView.Price))
        {
            _shopPanel.OpenSkin(_previousSkinView);
            SelectSkin();
            _previousSkinView.Unlock();
        }
    }

    private void OnAdButtonClick()
    {
        void CheckAd()
        {
            _saveManager.PlayerData.AdViewsForSkins[_previousSkinView.CharacterSkinItem.CharacterType]++;

            if (_saveManager.PlayerData.AdViewsForSkins[_previousSkinView.CharacterSkinItem.CharacterType] ==
                _previousSkinView.CharacterSkinItem.AmountOfAds)
            {
                _shopPanel.OpenSkin(_previousSkinView);
                SelectSkin();
                _previousSkinView.Unlock();
            }
        }
        
        YandexMain.Instance.ADManager.ShowRewardedAd(CheckAd);
    }

    private void SelectSkin()
    {
        _shopPanel.Select(_previousSkinView);
        ShowSelectedText();
    }

    private void OnSelectionButtonClick()
    {
        SelectSkin();
    }

    private void ShowSelectedText()
    {
        _selectedText.gameObject.Activate();
        HideSelectionButton();
        HideAdButton();
        HideBuyButton();
    }

    private void ShowSelectionButton()
    {
        _selectionButton.gameObject.Activate();
        HideSelectedText();
        HideBuyButton();
        HideAdButton();
    }

    private void ShowBuyButton(int price)
    {
        _buyButton.gameObject.Activate();
        _buyButton.UpdateText(price);

        if(_moneyCounter.HaveMoney(price))
            _buyButton.Unlock();
        else
            _buyButton.Lock();

        HideAdButton();
        HideSelectedText();
        HideSelectionButton();
    }

    private void ShowAdButton(int current, int max)
    {
        _buyForAdButton.gameObject.Activate();
        _buyForAdButton.UpdateText(current, max);
        
        HideBuyButton();
        HideSelectedText();
        HideSelectionButton();
    }

    private void CloseShop() => gameObject.SetActive(false);

    private void HideAdButton() => _buyForAdButton.gameObject.Deactivate();

    private void HideBuyButton() => _buyButton.gameObject.SetActive(false);
    private void HideSelectionButton() => _selectionButton.gameObject.SetActive(false);
    private void HideSelectedText() => _selectedText.gameObject.SetActive(false);
    }
}