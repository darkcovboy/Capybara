using UnityEngine;

namespace UI.ShopSkins
{
    [CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
    public class ShopItemFactory : ScriptableObject
    {
        [SerializeField] private SkinView _skinViewPrefab;

        public SkinView Get(CharacterSkinItem animalSkin, Transform parent)
        {
            SkinView instance = Instantiate(_skinViewPrefab, parent);
            instance.Initiliaze(animalSkin);
            return instance;
        }
    }
}