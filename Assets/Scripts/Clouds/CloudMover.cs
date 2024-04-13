using UnityEngine;

public class CloudMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position = Vector3.MoveTowards(_transform.position, _transform.position + _transform.forward, _speed * Time.deltaTime);
    }
}
