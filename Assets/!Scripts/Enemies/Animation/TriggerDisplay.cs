using System;
using UnityEngine;

namespace Enemies.Animation
{
    [ExecuteAlways]
    public class TriggerDisplay : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider _capsuleCollider;

        private void Update()
        {
            if(Application.isPlaying)
                return;
            
            SwitchScale();
        }

        private void SwitchScale()
        {
            var radius = _capsuleCollider.radius / 10;
            transform.localScale = new Vector3(radius, transform.localScale.y, radius);
        }
    }
}