using UnityEngine;

public class Performer : MonoBehaviour
{
    private Transform _targetTransform;
    private EmergencyCar _car;

    private void Awake()
    {
        _car = GetComponent<EmergencyCar>();
    }

    public void SetTask(Transform _taskTransform)
    {
        _targetTransform = _taskTransform;
        _car.Initialize(_targetTransform.position);
    }
}
