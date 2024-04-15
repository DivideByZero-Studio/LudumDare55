using UnityEngine;
using Zenject;

public class Department : MonoBehaviour
{
    [SerializeField] private EmergencyType _serviceType;
    [SerializeField] private GameObject _performerPrefab;

    [Inject] private DiContainer _diContainer;

    public EmergencyType ServiceType => _serviceType;

    public void PerformTask(Transform _targetTransform)
    {
        var performer = _diContainer.InstantiatePrefab(_performerPrefab, transform.position, Quaternion.identity, transform);
        performer.GetComponent<Performer>().SetTask(_targetTransform);
    }
}
