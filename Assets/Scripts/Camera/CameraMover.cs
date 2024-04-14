using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.GameCenter;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Camera _gameCamera;
    [SerializeField] private Camera _UICamera;
    
    [SerializeField] private float minCameraSize;
    [SerializeField] private float maxCameraSize;
    [SerializeField] private float scrollStep;

    private float _currentSize;
    private bool _shouldMove;

    private Vector3 _cameraCenterPosition;

    private float _allowedMoveDistance;
    private readonly float minAllowedMoveDistance = 1f;
    private readonly float maxAllowedMoveDistance = 300f;

    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Enable();

        _input.Player.ScrollUp.performed += OnMouseScrolledUp;
        _input.Player.ScrollDown.performed += OnMouseScrolledDown;
        _input.Player.MouseClick.started += OnMouseClick;
        _input.Player.MouseClick.canceled += OnMouseRelease;

        _currentSize = maxCameraSize;
        _allowedMoveDistance = minAllowedMoveDistance;
        _cameraCenterPosition = _gameCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
    }

    private void LateUpdate()
    {
        if (_shouldMove)
        {
            Move();
        }   
        ClampPositionWithBoundaries();
    }

    private void ClampPositionWithBoundaries()
    {
        var interpolationSize = Mathf.InverseLerp(minCameraSize, maxCameraSize, _currentSize);
        var distance = Vector3.Distance(transform.position, _cameraCenterPosition);
        _allowedMoveDistance = Mathf.Lerp(minAllowedMoveDistance, maxAllowedMoveDistance, 1-interpolationSize);

        if (distance > _allowedMoveDistance)
        {
            var direction = transform.position - _cameraCenterPosition;
            direction = direction * _allowedMoveDistance / distance;
            transform.position = _cameraCenterPosition + direction;
        }
    }

    private void Move()
    {
        var currentSizeMultiplier = _currentSize / maxCameraSize;
        var direction = GetMoveDirection() * currentSizeMultiplier;
        transform.Translate(direction.x, 0, direction.y);
    }

    private Vector2 GetMoveDirection()
    {
        return _input.Player.MouseDelta.ReadValue<Vector2>();
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        _shouldMove = true;
    }

    private void OnMouseRelease(InputAction.CallbackContext context)
    {
        _shouldMove = false;
    }

    private void OnMouseScrolledUp(InputAction.CallbackContext context)
    {
        _currentSize = Mathf.Clamp(_currentSize - scrollStep, minCameraSize, maxCameraSize);
        SetCamerasSize(_currentSize);
    }

    private void OnMouseScrolledDown(InputAction.CallbackContext context)
    {
        _currentSize = Mathf.Clamp(_currentSize + scrollStep, minCameraSize, maxCameraSize);
        SetCamerasSize(_currentSize);
    }

    private void SetCamerasSize(float size)
    {
        _gameCamera.orthographicSize = size;
        _UICamera.orthographicSize = size;
    }
}