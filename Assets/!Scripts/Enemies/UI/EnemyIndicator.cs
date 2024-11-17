using System;
using Extension;
using UnityEngine;
using Camera = UnityEngine.Camera;

namespace Enemies.UI
{
    public class EnemyIndicator : MonoBehaviour
    {
        private const float ScreenPositionCoordinate = 50f;
        
        private Transform _playerBody;
        private Transform _enemy;
        private EnemyPoint _indicator;

        private UnityEngine.Camera _mainCamera;

        public void Init(Transform playerBody, EnemyPoint enemyPoint, Transform enemy)
        {
            _playerBody = playerBody;
            _indicator = enemyPoint;
            _enemy = enemy;
        }

        private void Start()
        {
            _mainCamera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            SetPositionOnPlanes();
        }

        private void SetPositionOnPlanes()
        {
            Vector3 fromPlayerToEnemy = _enemy.position - _playerBody.position;
            Ray ray = new Ray(_playerBody.position, fromPlayerToEnemy);
            
            // [0] left, [1] right, [2] - down, [3] - up
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_mainCamera);

            float minDistance = Mathf.Infinity;
            int planeIndex = 0;

            for (int i = 0; i < 4; i++)
            {
                if(planes[i].Raycast(ray, out float distance))
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        planeIndex = i;
                    }
            }

            minDistance = Mathf.Clamp(minDistance, 0, fromPlayerToEnemy.magnitude);
            Vector3 worldPosition = ray.GetPoint(minDistance);
            
            if (fromPlayerToEnemy.magnitude > minDistance)
            {
                Vector3 screenPosition = _mainCamera.WorldToScreenPoint(worldPosition);

                screenPosition = DoOffsetPosition(planeIndex, screenPosition);

                _indicator.RectTransform.position = screenPosition;
                _indicator.RectTransform.rotation = GetRotation(planeIndex);
                _indicator.Show();
            }
            else
            {
                _indicator.Hide();
            }
        }

        private Vector3 DoOffsetPosition(int planeIndex, Vector3 screenPosition)
        {
            switch (planeIndex)
            {
                case 0: // Left
                    screenPosition.x += ScreenPositionCoordinate;
                    break;
                case 1: // Right
                    screenPosition.x -= ScreenPositionCoordinate;
                    break;
                case 2: // Bottom
                    screenPosition.y += ScreenPositionCoordinate;
                    break;
                case 3: // Top
                    screenPosition.y -= ScreenPositionCoordinate;
                    break;
            }

            return screenPosition;
        }

        private Quaternion GetRotation(int planeIndex)
        {
            switch (planeIndex)
            {
                case 0:
                    return Quaternion.Euler(0f, 0f, -90f);
                case 1:
                    return Quaternion.Euler(0f, 0f, 90f);
                case 2:
                    return Quaternion.Euler(0f, 0f, 0f);
                case 3:
                    return Quaternion.Euler(0f, 0f,180f);
                default:
                    return Quaternion.identity;
            }
        }
    }
}