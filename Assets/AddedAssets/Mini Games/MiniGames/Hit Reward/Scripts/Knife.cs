using System;
using System.Collections;
using UnityEngine;

namespace HitRewardSpace
{
    public class Knife : MonoBehaviour
    {
        private float _speed = 8;
        private Coroutine _coroutineMove;

        public event Action OnHit;

        public void Play() {
            _coroutineMove = StartCoroutine(MoveRoutine());
        }

        IEnumerator MoveRoutine() {
            while (true) {
                transform.Translate(Vector3.up * _speed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (_coroutineMove != null) {
                transform.SetParent(collision.transform.parent);
                transform.SetSiblingIndex(0);
                StopCoroutine(_coroutineMove);
                _coroutineMove = null;

                OnHit?.Invoke();
            }
        }

        public void SetSpeed(float speed) {
            _speed = speed;
        }
    }
}
