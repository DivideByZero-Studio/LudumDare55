using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Camera _gameCamera;
    [SerializeField] private Camera _UICamera;
    
    [SerializeField] private float minCameraSize;
    [SerializeField] private float maxCameraSize;
    [SerializeField] private float scrollStep;
    [SerializeField] private float scrollSpeed;

    private float _currentSize;
    private bool _shouldMove;

    private Vector3 _cameraCenterPosition;
    private Vector3 startMousePosition;

    private float _allowedMoveDistance;
    private readonly float minAllowedMoveDistance = 1f;
    private readonly float maxAllowedMoveDistance = 300f;

    private PlayerInput _input;
    private Coroutine _sizeSetRoutine;

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

    private void Update()
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
        var currentMousePosition = _gameCamera.ScreenToWorldPoint(_input.Player.MousePosition.ReadValue<Vector2>());
        var difference = currentMousePosition - transform.position;
        transform.position = startMousePosition - difference;
    }

    private Vector2 GetMoveDirection()
    {
        return _input.Player.MouseDelta.ReadValue<Vector2>();
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        startMousePosition = _gameCamera.ScreenToWorldPoint(_input.Player.MousePosition.ReadValue<Vector2>());
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
        if (_sizeSetRoutine != null)
            StopCoroutine(_sizeSetRoutine);
        _sizeSetRoutine = StartCoroutine(SetCamerasSizeRoutine(size));
    }

    private IEnumerator SetCamerasSizeRoutine(float size)
    {
        float time = 1 / scrollSpeed;
        float currentSize = _gameCamera.orthographicSize;
        float progress = 0;
        while (time > 0)
        {
            progress += scrollSpeed * Time.deltaTime;
            currentSize = Mathf.Lerp(currentSize, size, progress);
            _gameCamera.orthographicSize = currentSize;
            _UICamera.orthographicSize = currentSize;
            time -= Time.deltaTime;
            yield return null;
        }
    }
}