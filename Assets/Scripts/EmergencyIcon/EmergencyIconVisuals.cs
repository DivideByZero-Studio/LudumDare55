using UnityEngine;
using Zenject;

[RequireComponent(typeof(ClickReciever))]
public class EmergencyIconVisuals : MonoBehaviour
{
    [Inject] private Camera _camera;

    [SerializeField] private Animator _animator;

    private Transform _transform;
    private float _maxCameraOrthographicSize;

    private void Awake()
    {
        _transform = transform;
        _maxCameraOrthographicSize = _camera.orthographicSize;
    }

    public void ZoomIn()
    {
        _animator.SetBool("Zoomed", true);
    }

    public void ZoomOut()
    {
        _animator.SetBool("Zoomed", false);
    }

    private void Update()
    {
        _transform.LookAt(_camera.transform);
        _transform.localScale = new Vector3(1f / (_maxCameraOrthographicSize / _camera.orthographicSize), 1f / (_maxCameraOrthographicSize / _camera.orthographicSize), 1f / (_maxCameraOrthographicSize / _camera.orthographicSize));
    }
}
