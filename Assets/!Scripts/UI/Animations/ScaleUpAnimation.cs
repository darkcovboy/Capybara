using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.Animations
{
    public class ScaleUpAnimation : MonoBehaviour
    {
        [SerializeField] private Mode _mode= Mode.OnEnable;
        [SerializeField] private bool _hasDelay;
        [SerializeField] [ShowIf(nameof(_hasDelay))] [Min(0f)]
        private float _delay= 0.5f;
        [SerializeField] [Min(0f)] private float _duration= 0.5f;
        [SerializeField] private Ease _ease= Ease.OutBack;
        [SerializeField] [Min(0f)] private float _overshoot= 1.7f;
        private void Start()
        {
            if (_mode != Mode.Start)
                return;

            Scale(transform.localScale);
        }

        private void OnEnable()
        {
            if (_mode != Mode.OnEnable)
                return;

            
            Scale(Vector3.one);
        }

        private void Scale(Vector3 endValue)
        {
            this.DOKill();
            transform.localScale = Vector3.zero;
            var scaleTween = transform.DOScale(endValue, _duration).SetEase(_ease, _overshoot);
            if (_hasDelay)
                DOTween.Sequence().SetRecyclable(true).AppendInterval(_delay).Append(scaleTween).SetId(this);
            else
                scaleTween.SetId(this).SetRecyclable(true);
        }

        private enum Mode
        {
            OnEnable,
            Start,
        }
    }
}