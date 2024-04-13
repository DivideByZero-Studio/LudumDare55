using System;
using UnityEngine;

public class ChooseServiceButton : MonoBehaviour
{
    public event Action<EmergencyType> Choosed;

    [SerializeField] private EmergencyType _emergencyType;

    public void OnClicked()
    {
        if (_emergencyType == EmergencyType.NonType)
        {
            throw new NullReferenceException(nameof(_emergencyType));
        }
        Choosed?.Invoke(_emergencyType);
    }
}