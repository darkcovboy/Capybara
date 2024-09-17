using System.Collections;
using Extension;
using Items;
using UnityEngine;

namespace Enemies.Catcher
{
    public class Cage : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Transform _centrePosition;
        [SerializeField] private Vector3 _targetScale;
        [SerializeField] private Collider _item;
        [SerializeField] private Rigidbody _rigidbody;

        public void Take(Transform character)
        {
            Debug.Log("Take");
            character.SetParent(_centrePosition);
            character.localPosition = Vector3.zero;
            character.localScale = _targetScale;
            _particleSystem.gameObject.Activate();
            StartCoroutine(CanBeTaked());
        }

        private IEnumerator CanBeTaked()
        {
            yield return new WaitForSeconds(1f);

            _item.enabled = true;
            _rigidbody.isKinematic = false;
        }
    }
}