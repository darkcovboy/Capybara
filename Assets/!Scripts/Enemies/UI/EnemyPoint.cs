using Extension;
using UnityEngine;

namespace Enemies.UI
{
    public class EnemyPoint : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform;

        public void Show()
        {
            gameObject.Activate();
        }

        public void Hide()
        {
            gameObject.Deactivate();
        }
    }
}