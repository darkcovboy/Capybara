using System;
using UnityEngine;

namespace Camera
{
    public class LookAtCamera : MonoBehaviour
    {
        
        private void Awake()
        {
            Vector3 direction = UnityEngine.Camera.main.transform.position - transform.position;
            transform.LookAt(transform.position + direction, Vector3.up);
            transform.Rotate(0, 180, 0);
        }
    }
}