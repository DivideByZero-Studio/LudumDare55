using UnityEngine;
using Zenject;

[RequireComponent(typeof(ClickReciever), typeof(BaseEmergency))]
public class EmergencyIcon : MonoBehaviour
{
    private ClickReciever _clickReciever;
    private BaseEmergency _emergency;
    private EmergencyIconVisuals _visuals;

    [Inject] ChooseServicePanel _chooseServicePanel;

    private void Awake()
    {
        _clickReciever = GetComponent<ClickReciever>();
        _emergency = GetComponent<BaseEmergency>();
        _visuals = GetComponent<EmergencyIconVisuals>();
    }
    private void InvokeChoosePanel()
    {
        _chooseServicePanel.Activate(this);
    }

    public EmergencyType GetEmergencyType()
    {
        return _emergency.Type;
    }

    public void ActivateChoosenEffect()
    {
        _visuals.ZoomIn();
    }

    public void DeactivateChossenEffect()
    {
        _visuals.ZoomOut();
    }

    private void OnEnable()
    {
        _clickReciever.Clicked += InvokeChoosePanel;
    }
   
    private void OnDisable()
    {
        _clickReciever.Clicked -= InvokeChoosePanel;
    }
}
