using System;
using UnityEngine;

namespace DropGame
{
    public class CoinObject : MonoBehaviour
    {
        public int RewardIndex { get; private set; }
        [SerializeField] private GameController _gameController;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private PhysicsMaterial2D _materialBounce;

        private bool collisitonDetected;

        public event Action OnResultObtained;


        private void SetRewardIndex(int index) {
            collisitonDetected = true;
            RewardIndex = index;
            OnResultObtained?.Invoke();
            _rigidbody2D.sharedMaterial = null;
        }

        public void Drop() {
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2D.sharedMaterial = _materialBounce;
        }

        public void SetGravityScale(float gravity) {
            _rigidbody2D.gravityScale = gravity;
        }

        public void Reset() {
            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            collisitonDetected = false;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!collisitonDetected) {
                if (collision.GetComponent<Slot>() is Slot slot)
                    SetRewardIndex(slot.Index);
            }
        }
    }
}
