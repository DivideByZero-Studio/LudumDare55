using UnityEngine;

public class Department : MonoBehaviour
{
    [SerializeField] private EmergencyType _serviceType;
    [SerializeField] private GameObject _performerPrefab;
    public EmergencyType ServiceType => _serviceType;
    public void PerformTask(Transform _targetTransform)
    {
        var performer = Instantiate(_performerPrefab, transform.position, Quaternion.identity);
        performer.GetComponent<Performer>().SetTask(_targetTransform);
    }
}
