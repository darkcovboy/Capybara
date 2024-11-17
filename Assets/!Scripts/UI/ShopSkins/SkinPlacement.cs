using System;
using Player.Skins;
using UnityEngine;

namespace UI.ShopSkins
{
    public class SkinPlacement : MonoBehaviour
    {
        public event Action<Transform> OnModelCreated;
        public event Action OnModelDeleted;

        [SerializeField] private CardFactory _cardFactory;

        private Card _currentModel;

        
        public void InstantiateModel(int stars, string modelName, ViewModel model)
        {
            DestroyModel();
            
            _currentModel = Instantiate(_cardFactory.Get(stars), transform);
            _currentModel.UpdateText(modelName);
            
            Instantiate(model, _currentModel.SkinPlacement);
            
            OnModelCreated?.Invoke(_currentModel.transform);
        }

        public void DestroyModel()
        {
            if (_currentModel != null)
            {
                OnModelDeleted?.Invoke();
                Destroy(_currentModel.gameObject);
            }
        }
    }
}