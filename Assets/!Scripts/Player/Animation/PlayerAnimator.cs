﻿using System;
using UnityEngine;

namespace Player.Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private readonly int _moveSpeed = Animator.StringToHash("Speed");
        private readonly int _haveItem = Animator.StringToHash("HaveItem");
        private readonly int _victory = Animator.StringToHash("Victory");
        private readonly int _death = Animator.StringToHash("Death");

        private void OnValidate()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(_moveSpeed, speed);
        }
        
        public void SetItem(bool haveItem)
        {
            _animator.SetBool(_haveItem, haveItem);
        }

        public void PlayVictory()
        {
            _animator.SetTrigger(_victory);
        }

        public void PlayDeath()
        {
            _animator.SetTrigger(_death);
        }
    }
}