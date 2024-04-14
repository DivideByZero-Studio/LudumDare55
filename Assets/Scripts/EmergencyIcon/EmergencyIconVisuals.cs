using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(ClickReciever))]
public class EmergencyIconVisuals : MonoBehaviour
{
    [Inject] private Camera _camera;

    [SerializeField] private Animator _animator;
    [SerializeField] private Canvas _canvas;

    private Transform _transform;
    private float _maxCameraOrthographicSize;

    private Collider _collider;

    private void Awake()
    {
        _transform = transform;
        _maxCameraOrthographicSize = CameraExtensions.MaxOrthographicSize;
        _collider = GetComponent<Collider>();
    }

    public void ZoomIn()
    {
        _animator.SetBool("Zoomed", true);
    }

    public void ZoomOut()
    {
        _animator.SetBool("Zoomed", false);
    }

    public void Deactivate()
    {
        _canvas.gameObject.SetActive(false);
        _collider.enabled = false;
    }

    private void Update()
    {
        _transform.LookAt(_camera.transform);
        _transform.localScale = new Vector3(1f / (_maxCameraOrthographicSize / _camera.orthographicSize), 1f / (_maxCameraOrthographicSize / _camera.orthographicSize), 1f / (_maxCameraOrthographicSize / _camera.orthographicSize));
    }
}
