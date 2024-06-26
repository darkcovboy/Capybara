﻿using Player.Skins;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.ShopSkins
{
    [CreateAssetMenu(fileName = "Animal Skin", menuName = "Animal Skin Item")]
    public class CharacterSkinItem : ScriptableObject
    {
        [Header("ShopInfo")]
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private bool _isBuingForAds = false;
        [SerializeField] private int _price;
        [ShowIf("_isBuingForAds")]
        [SerializeField] private int _amountOfAds = 1;
        [SerializeField] private Sprite _image;
        [SerializeField] private ViewModel _viewModel;

        public CharacterType CharacterType => _characterType;
        public int Price => _price;
        public Sprite Image => _image;
        public ViewModel ViewModel => _viewModel;

        public bool IsBuingForAds => _isBuingForAds;

        public int AmountOfAds => _amountOfAds;
    }
}