using System;
using System.Collections;
using Const;
using DG.Tweening;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Item : MonoBehaviour
    {
        [SerializeField] private RewardType _rewardType;

        [SerializeField] private Collider _collider;

        [SerializeField] private Rigidbody _rigidbody;

        private Quaternion _initialRotation;

        private float _moveDutation = 6f;

        public ItemData ItemData { get; private set; }

        private void OnValidate()
        {
            if (_collider == null)
                _collider = GetComponent<Collider>();
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _initialRotation = transform.rotation;
            ItemData = LoadData();
            
            if(ItemData == null)
                throw new Exception($"Problem with item data in object {gameObject.name}");
        }

        public void ConnectTo(Transform at, bool isCollector = false)
        {
            _collider.enabled = false;
            _rigidbody.isKinematic = true;
            
            if(isCollector)
                MoveToCollector(at);
            else
                StartCoroutine(MoveToTarget(at));
        }

        public void Disconnect()
        {
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
            transform.SetParent(null);
        }

        private void DestroySelf()
        {
            Disconnect();
            Destroy(gameObject);
        }

        private ItemData LoadData()
        {
            switch (_rewardType)
            {
                case RewardType.Easy:
                    return Resources.Load<ItemData>(Paths.EasyItemPath);
                case RewardType.Medium:
                    return Resources.Load<ItemData>(Paths.MediumItemPath);
                case RewardType.Hard:
                    return Resources.Load<ItemData>(Paths.HardItemPath);
            }

            return null;
        }

        private void MoveToCollector(Transform target)
        {
            transform.SetParent(null);
            transform.DOMove(target.position, _moveDutation).SetEase(Ease.InOutSine).OnComplete(DestroySelf);
        }

        private IEnumerator MoveToTarget(Transform target)
        {
            transform.DORotateQuaternion(_initialRotation, 0.5f);

            while (Vector3.Distance(transform.position, target.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 15f);
                yield return null;
            }
            
            transform.SetParent(target);
        }
    }

    public enum RewardType
    {
        Easy,
        Medium,
        Hard
    }
}