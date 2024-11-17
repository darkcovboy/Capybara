using System;
using Player.Movement;
using UnityEngine;

namespace DefaultNamespace.Test
{
    public class ObjectMoverTest : MonoBehaviour
    {
        private IInput _input;
        [SerializeField] private float _speed;

        private void Start()
        {
            //_speed = 6f;
            _input = new DesktopInput();
        }

        private void Update()
        {
            Vector3 direction = _input.GetInputDirection();
            Move(direction);
        }

        private void Move(Vector3 direction)
        {
            transform.position += direction * Time.deltaTime * _speed;
        }
    }
}