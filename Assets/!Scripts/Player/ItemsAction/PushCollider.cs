using UnityEngine;

namespace Player.ItemsAction
{
    public class PushCollider : MonoBehaviour
    {
        [SerializeField] private float _pushForce = 5f;

        private void OnCollisionEnter(Collision collision)
        {
            Rigidbody attachedRigidbody = collision.collider.attachedRigidbody;

            if (attachedRigidbody != null && !attachedRigidbody.isKinematic)
            {
                Vector3 pushDirection = collision.transform.position - transform.position;
                pushDirection.y = 0;

                attachedRigidbody.AddForce(pushDirection.normalized * _pushForce, ForceMode.Impulse);
            }
        }
    }
}