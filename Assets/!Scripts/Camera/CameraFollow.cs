using System;
using Player;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _smoothSpeed = 5f;
        [SerializeField] private Vector3 _offset;
        
        private IWatch _watchObject;

        [Inject]
        public void Constructor(IWatch watch)
        {
            _watchObject = watch;
        }

        private void LateUpdate()
        {
            if (_watchObject != null)
            {
                Vector3 desiredPosition = _watchObject.Transform.position + _offset;
                
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);

                transform.position = smoothedPosition;
            }
        }
    }
}