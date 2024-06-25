using Player.Skins;
using UnityEngine;

namespace UI.ShopSkins
{
    [CreateAssetMenu(fileName = "Animal Skin", menuName = "Animal Skin Item")]
    public class CharacterSkinItem : ScriptableObject
    {
        [Header("ShopInfo")]
        [SerializeField] private CharacterType _characterType;
        [SerializeField, Range(0,10000)] private int _price;
        [SerializeField] private Sprite _image;
        [SerializeField] private ViewModel _viewModel;

        public CharacterType CharacterType => _characterType;
        public int Price => _price;
        public Sprite Image => _image;
        public ViewModel ViewModel => _viewModel;
    }
}