using System;
using System.Collections.Generic;
using Player.Movement;
using Player.PlayerStaticData;
using UnityEngine;
using Zenject;

namespace Player
{
    public class ObjectMover : MonoBehaviour
    {
        private IInput _input;
        private float _speed;

        [Inject]
        public void Constructor(CharacterData characterData, IInput input)
        {
            _speed = characterData.Speed;
            _input = input;
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