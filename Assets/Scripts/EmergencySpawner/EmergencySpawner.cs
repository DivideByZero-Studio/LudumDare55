using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EmergencySpawner : MonoBehaviour
{
    public bool Occupied => _occupied;

    private bool _occupied = false;

    [SerializeField] private Transform _iconParentTransform;
    [SerializeField] private List<GameObject> _emergencyIconPrefabs;

    [Inject] private DiContainer _diContainer;

    private EmergencyIcon _currentIcon;


    public void SpawnByType(EmergencyType type)
    {
        if (_currentIcon != null)
            return;

        _currentIcon = _diContainer.InstantiatePrefab(GetIcon(type), _iconParentTransform).GetComponentInChildren<EmergencyIcon>();
        _currentIcon.Initialize(transform);
        _currentIcon.Died += Unoccupie;
    }

    private GameObject GetIcon(EmergencyType type)
    {
        foreach (var icon in _emergencyIconPrefabs)
        {
            if (icon.GetComponentInChildren<BaseEmergency>().Type == type)
            {
                return icon;
            }
        }
        return null;
    }

    private void Unoccupie()
    {
        _currentIcon = null;
        _occupied = false;
    }
}
