using System;
using System.Collections.Generic;
using UnityEngine;

public class ChooseServicePanel : MonoBehaviour
{
    public event Action<EmergencyIcon, EmergencyType> EmergencyDone;
    public event Action EmergencyFailed;

    [SerializeField] private List<ChooseServiceButton> _chooseButtons;

    private EmergencyIcon _currentIcon;
    private bool _isActive = false;
    private EmergencyType _expectedEmergencyType;
    private ChooseServicePanelVisuals _visuals;

    private void Awake()
    {
        _visuals = GetComponent<ChooseServicePanelVisuals>();
    }
    
    public void Activate(EmergencyIcon icon)
    {
        if (icon == _currentIcon)
            return;

        _visuals.Activate();
        _currentIcon?.DeactivateChossenEffect();
        _currentIcon = icon;
        _currentIcon.ActivateChoosenEffect();
        
        _expectedEmergencyType = _currentIcon.GetEmergencyType();

        if (_isActive)
            return;

        _isActive = true;
    }

    public void Deactivate()
    {
        _visuals.Deactivate();
        _currentIcon?.DeactivateChossenEffect();
        _currentIcon = null;
        _expectedEmergencyType = EmergencyType.NonType;
        _isActive = false;
    }

    private void OnChoosed(EmergencyType emergencyType)
    {
        if (_expectedEmergencyType != emergencyType)
        {
            EmergencyFailed?.Invoke();
            Deactivate();
            return;
        }

        EmergencyDone?.Invoke(_currentIcon, emergencyType);
        Deactivate();
    }

    private void OnEnable()
    {
        foreach (ChooseServiceButton button in _chooseButtons)
        {
            button.Choosed += OnChoosed;
        }
    }

    private void OnDisable()
    {
        foreach (ChooseServiceButton button in _chooseButtons)
        {
            button.Choosed -= OnChoosed;
        }
    }
}
