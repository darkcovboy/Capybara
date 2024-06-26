using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class ScaleLoopAnimation : MonoBehaviour
    {
        [SerializeField] private float _bigScaleMultiplier= 1.25f;
        [SerializeField] [Min(0f)] private float _period= 0.25f;
        [SerializeField] private Ease _ease= Ease.InOutQuad;
        [SerializeField] [Min(0f)] private float _delay;

        private Vector3 _initialScale;

        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        private void OnEnable()
        {
            this.DOKill();
            transform.localScale = Vector3.zero;
            var halfPeriod = _period * 0.5f;
            var bigScale = _initialScale * _bigScaleMultiplier;
            DOTween.Sequence().SetId(this).SetRecyclable(true)
                .AppendInterval(_delay)
                .Append(transform.DOScale(bigScale, halfPeriod).SetEase(_ease))
                .Append(transform.DOScale(_initialScale, halfPeriod).SetEase(_ease)
                    .SetLoops(int.MaxValue, LoopType.Yoyo)
                );
        }

        private void OnDisable()
        {
            this.DOKill();
        }
    }
}