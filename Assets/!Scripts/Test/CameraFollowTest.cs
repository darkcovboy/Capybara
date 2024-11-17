using UnityEngine;

namespace DefaultNamespace.Test
{
    public class CameraFollowTest : MonoBehaviour
    {
        [SerializeField] private float _smoothSpeed = 5f;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _watchObject;

        private void LateUpdate()
        {
            if (_watchObject != null)
            {
                Vector3 desiredPosition = _watchObject.position + _offset;
                
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);

                transform.position = smoothedPosition;
            }
        }
    }
}