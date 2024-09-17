using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Items.Collector.Numbers
{
    public class NumberObject : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _animationDuration = 1f;
        [SerializeField] private Vector3 _endPositionOffset;
        [SerializeField] private Vector3 _endScale = new Vector3(1.5f, 1.5f, 1.5f);

        private Vector3 _initialPosition;
        private Vector3 _initialScale;

        private void Awake()
        {
            _initialPosition = transform.position;
            _initialScale = transform.localScale;
        }

        public void Initialize(int value, Color color)
        {
            _text.outlineColor = color;
            _text.text = value.ToString();
            Animate();
        }

        private void Animate()
        {
            transform.DOMove(transform.position + _endPositionOffset, _animationDuration);
            transform.DOScale(_endScale, _animationDuration).OnComplete(ReturnToPool);
        }

        private void ReturnToPool()
        {
            transform.position = _initialPosition;
            transform.localScale = _initialScale;
            gameObject.SetActive(false);
        }
    }
}