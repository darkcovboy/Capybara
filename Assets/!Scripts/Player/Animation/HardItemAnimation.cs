using Items;
using Player.ItemsAction;
using UnityEngine;

namespace Player.Animation
{
    public class HardItemAnimation : MonoBehaviour
    {
        [SerializeField] private ItemHandler _itemHandler;
        [SerializeField] private ParticleSystem _particleSystem;

        public void Start()
        {
            OffParticle();
            _itemHandler.OnItemCollected += OnItemTaked;
        }

        private void OnItemTaked(RewardType? weight)
        {
            if (weight == null)
                OffParticle();
            else if (weight == RewardType.Hard)
                OnParticle();
        }

        private void OnParticle() => _particleSystem.gameObject.SetActive(true);
        private void OffParticle() => _particleSystem.gameObject.SetActive(false);

    }
}