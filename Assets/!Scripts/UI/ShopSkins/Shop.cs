using Player.Counter;
using Sirenix.OdinInspector;
using UI.Buttons;
using UnityEngine;
using Zenject;

namespace UI.ShopSkins
{
    public class Shop : MonoBehaviour
    {
         [SerializeField] private ShopContent _contentItems;

    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private Button _selectionButton;
    [SerializeField] private Image _selectedText;

    [SerializeField] private Button _exitButton;

    [SerializeField] private SkinPlacement _skinPlacement;

    [SerializeField] private ShopPanel _shopPanel;

    private SkinView _previousSkinView;

    private MoneyCounter _moneyCounter;

    private void OnEnable()
    {
        _shopPanel.Show(_contentItems.CharacterSkins.Cast<AnimalSkinItem>());
        _shopPanel.SkinViewClicked += OnItemViewClicked;
        _buyButton.Click += OnBuyButtonClick;
        _selectionButton.onClick.AddListener(OnSelectionButtonClick);
        _exitButton.onClick.AddListener(CloseShop);
    }
    private void OnDisable()
    {
        _shopPanel.SkinViewClicked -= OnItemViewClicked;
        _buyButton.Click -= OnBuyButtonClick;
        _selectionButton.onClick.RemoveListener(OnSelectionButtonClick);
        _exitButton.onClick.RemoveListener(CloseShop);
    }

    [Inject]
    public void Constructor(MoneyCounter moneyCounter)
    {
        _moneyCounter = moneyCounter;
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
            ShowBuyButton(item.Price);
        }
    }

    private void OnBuyButtonClick()
    {
        if(_moneyCounter.IsEnough(_previousSkinView.Price))
        {
            _moneyCounter.Spend(_previousSkinView.Price);
            _shopPanel.OpenSkin(_previousSkinView);
            SelectSkin();
            _previousSkinView.Unlock();
        }
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
        HideBuyButton();
    }

    private void ShowSelectionButton()
    {
        _selectionButton.gameObject.Activate();
        HideSelectedText();
        HideBuyButton();
    }

    private void ShowBuyButton(int price)
    {
        _buyButton.gameObject.Activate();
        _buyButton.UpdateText(price);

        if(_moneyCounter.IsEnough(price))
            _buyButton.Unlock();
        else
            _buyButton.Lock();

        HideSelectedText();
        HideSelectionButton();
    }

    [Button]
    private void CloseShop() => gameObject.SetActive(false);

    private void HideBuyButton() => _buyButton.gameObject.SetActive(false);
    private void HideSelectionButton() => _selectionButton.gameObject.SetActive(false);
    private void HideSelectedText() => _selectedText.gameObject.SetActive(false);
    }
}