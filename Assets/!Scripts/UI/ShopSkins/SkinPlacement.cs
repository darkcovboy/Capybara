using Player.Skins;
using UnityEngine;

namespace UI.ShopSkins
{
    public class SkinPlacement : MonoBehaviour
    {
        private const string RenderLayer = "SkinRender";

        private ViewModel _currentModel;

        public void InstantiateModel(ViewModel model)
        {
            if (_currentModel != null)
                Destroy(_currentModel.gameObject);

            _currentModel = Instantiate(model, transform);

            Transform[] childrens = _currentModel.GetComponentsInChildren<Transform>();
            foreach (Transform child in childrens)
                child.gameObject.layer = LayerMask.NameToLayer(RenderLayer);
        }
    }
}