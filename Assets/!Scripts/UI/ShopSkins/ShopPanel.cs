using System;
using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using UnityEngine;
using Zenject;

namespace UI.ShopSkins
{
    public class ShopPanel : MonoBehaviour
    {
         public event Action<SkinView> SkinViewClicked;
    private List<SkinView> _skinViews = new List<SkinView>();

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ShopItemFactory _itemFactory;

    private SaveManager _saveManager;

    [Inject]
    public void Constructor(SaveManager saveManager)
    {
        _saveManager = saveManager;
    }

    public void Show(IEnumerable<CharacterSkinItem> skinItems)
    {
        Clear();

        foreach (var skinItem in skinItems)
        {
            SkinView skinView = _itemFactory.Get(skinItem, _itemsParent);

            skinView.Click += OnSkinItemViewClick;

            skinView.Unselect();
            skinView.UnHighlight();

            if(_saveManager.PlayerData.UnlockedSkins.Contains(skinView.CharacterSkinItem.CharacterType))
            {
                if (skinView.CharacterSkinItem.CharacterType == _saveManager.PlayerData.SelectedSkin)
                {
                    skinView.Select();
                    skinView.Highlight();
                    SkinViewClicked?.Invoke(skinView);
                }

                skinView.Unlock();
            }
            else
            {
                skinView.Lock();
            }

            _skinViews.Add(skinView);
        }

        Sort();
    }

    public void OpenSkin(SkinView skinView)
    {
        _saveManager.OpenNewSkin(skinView.CharacterSkinItem.CharacterType);
    }

    public void Select(SkinView skinView)
    {
        foreach (var skinItem in _skinViews)
            skinItem.Unselect();

        skinView.Select();
        _saveManager.SelectSkin(skinView.CharacterSkinItem.CharacterType);
    }

    private void Sort()
    {
        _skinViews = _skinViews
            .OrderBy(item => item.IsLock)
            .ThenByDescending(item => item.Price)
            .ToList();

        for (int i = 0; i < _skinViews.Count; i++)
            _skinViews[i].transform.SetSiblingIndex(i);
    }


    private void Clear()
    {
        foreach (SkinView item in _skinViews)
        {
            item.Click -= OnSkinItemViewClick;
            Destroy(item.gameObject);
        }

        _skinViews.Clear();
    }

    private void OnSkinItemViewClick(SkinView view)
    {
        Hightlight(view);
        SkinViewClicked?.Invoke(view);
    }

    private void Hightlight(SkinView skinView)
    {
        foreach (var skin in _skinViews)
            skin.UnHighlight();

        skinView.Highlight();
    }
    }
}