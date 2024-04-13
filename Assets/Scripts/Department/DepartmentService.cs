using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DepartmentService : MonoBehaviour
{
    [Inject] private ChooseServicePanel _chooseServicePanel;

    [SerializeField] private List<Department> _departments;

    private void CreateDepartmentRequest(Transform _targetTransform, EmergencyType emergencyType)
    {
        var department = ChooseDepartment(emergencyType);
        if (department == null) throw new ArgumentNullException(nameof(emergencyType));
        department.PerformTask(_targetTransform);
    }

    private Department ChooseDepartment(EmergencyType emergencyType)
    {
        foreach (var department in _departments)
        {
            if (department.ServiceType == emergencyType)
            {
                return department;
            }
        }
        return null;
    }

    private void OnEnable()
    {
        _chooseServicePanel.EmergencyDone += CreateDepartmentRequest;
    }


    private void OnDisable()
    {
        _chooseServicePanel.EmergencyDone -= CreateDepartmentRequest;
    }
}
