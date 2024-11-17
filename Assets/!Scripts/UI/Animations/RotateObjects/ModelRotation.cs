using System.Collections.Generic;
using UI.ShopSkins;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Animations.RotateObjects
{
    public class ModelRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private Vector2 _rotationXLimits = new Vector2(-10f, 10f);
        [SerializeField] private Vector2 _rotationYLimits = new Vector2(-10f, 10f);

        [Header("Dependencies")] [SerializeField]
        private SkinPlacement _skinPlacement;

        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private EventSystem _eventSystem;


        private Transform _model;
        private bool _isDragging = false;
        private Vector3 _lastMousePosition;
        private Vector3 _currentRotation;
        private bool _isMobileDevice;

        private void OnEnable()
        {
            _skinPlacement.OnModelCreated += AddModel;
            _skinPlacement.OnModelDeleted += DeleteModel;
        }

        private void OnDisable()
        {
            _skinPlacement.OnModelCreated -= AddModel;
            _skinPlacement.OnModelDeleted -= DeleteModel;
        }

        private void AddModel(Transform model)
        {
            _model = model;
        }

        private void DeleteModel()
        {
            _model = null;
        }

        private void Update()
        {
            if (_model == null) return;

            if (_isMobileDevice)
            {
                HandleTouchInput();
            }
            else
            {
                HandleMouseInput();
            }
        }

        private void HandleMouseInput()
        {
            if (IsPointerOverUIElement())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartDragging(Input.mousePosition);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    StopDragging();
                }
            }
            else
            {
                StopDragging();
            }

            if (_isDragging)
            {
                RotateModel(Input.mousePosition);
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (IsPointerOverUIElement())
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        StartDragging(touch.position);
                    }
                    else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        StopDragging();
                    }
                }
                else
                {
                    StopDragging();
                }

                if (_isDragging && touch.phase == TouchPhase.Moved)
                {
                    RotateModel(touch.position);
                }
            }
        }

        private void StartDragging(Vector3 startPosition)
        {
            _isDragging = true;
            _lastMousePosition = startPosition;
            _currentRotation = _model.localEulerAngles;
        }

        private void StopDragging()
        {
            _isDragging = false;
        }

        private void RotateModel(Vector3 currentPosition)
        {
            Vector3 deltaMousePosition = currentPosition - _lastMousePosition;

            float rotationX = deltaMousePosition.y * _rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMousePosition.x * _rotationSpeed * Time.deltaTime;

            _currentRotation.x = Mathf.Clamp(_currentRotation.x + rotationX, _rotationXLimits.x, _rotationXLimits.y);
            _currentRotation.y = Mathf.Clamp(_currentRotation.y + rotationY, _rotationYLimits.x, _rotationYLimits.y);

            _model.localEulerAngles = _currentRotation;

            _lastMousePosition = currentPosition;
        }

        private bool IsPointerOverUIElement()
        {
            PointerEventData pointerData = new PointerEventData(_eventSystem)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            _raycaster.Raycast(pointerData, results);

            return results.Exists(r => r.gameObject.GetComponent<RawImage>() != null);
        }
    }
}