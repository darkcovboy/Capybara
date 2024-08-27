using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class ScaleDownAnimation : MonoBehaviour
    {
        [SerializeField] [Min(0f)] private float _duration= 0.5f;
        [SerializeField] private Ease _ease = Ease.Linear;

        public float Duration => _duration;

        public void ScaleDown()
        {
            this.DOKill();
            Vector3 endValue = Vector3.zero;
            transform.localScale = Vector3.one;
            var scaleTween = transform.DOScale(endValue, _duration).SetEase(_ease);
            scaleTween.SetId(this).SetRecyclable(true);
        }
    }
}