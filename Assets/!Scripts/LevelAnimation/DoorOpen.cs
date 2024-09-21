using System;
using DG.Tweening;
using Player;
using UnityEngine;

namespace LevelAnimation
{
    public class DoorOpen : MonoBehaviour
    {
        [SerializeField] private Transform _doorRight;
        [SerializeField] private Transform _doorLeft;

        private float _angle = 160f;
        private bool _isOpen = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if(!_isOpen)
                return;

            if (!other.GetComponent<Character>()) return;
            Quaternion rotationRight = Quaternion.Euler(new Vector3(0f, _angle, 0f));
            Quaternion rotationLeft = Quaternion.Euler(new Vector3(0f, -_angle, 0f));
            _doorRight.DOLocalRotate(rotationRight.eulerAngles, 0.5f).SetEase(Ease.InOutSine);
            _doorLeft.DOLocalRotate(rotationLeft.eulerAngles, 0.5f).SetEase(Ease.InOutSine);
        }
    }
}