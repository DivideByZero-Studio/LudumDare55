using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ClickReciever), typeof(BaseEmergency))]
public class EmergencyIcon : MonoBehaviour
{
    public event Action Died; 

    public Transform EmergencyPointTransform => _emergencyPointTransform;

    [SerializeField] private float _maxLifeTime;
    [SerializeField] private float _particleSystemLifeTime;

    private Transform _emergencyPointTransform;
    private ClickReciever _clickReciever;
    private BaseEmergency _emergency;
    private EmergencyIconVisuals _visuals;

    [Inject] ChooseServicePanel _chooseServicePanel;
    [Inject] EmergencySpawnService _emergencySpawnService;
    [Inject] TimeCountService _timeCountService;

    private void Awake()
    {
        _clickReciever = GetComponent<ClickReciever>();
        _emergency = GetComponent<BaseEmergency>();
        _visuals = GetComponent<EmergencyIconVisuals>();
    }

    private void Start()
    {
        StartCoroutine(LifeRoutine());
    }

    private void InvokeChoosePanel()
    {
        _chooseServicePanel.Activate(this);
    }

    public void Initialize(Transform emergencyPointTransform)
    {
        _emergencyPointTransform = emergencyPointTransform;
    }

    public void PlayDoneEffect()
    {
        StopAllCoroutines();
        StartCoroutine(ParticleRoutine());
        _visuals.Deactivate();
        //Play Done effect
    }

    public void PlayFailedEffect()
    {
        StopAllCoroutines();
        StartCoroutine(ParticleRoutine());
        _visuals.Deactivate();
        //Play Failed effect
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

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(_maxLifeTime / _emergencySpawnService._difficultyMultiplier.Evaluate(_timeCountService.TimePassed));
        var activeEmergencyIcon = _chooseServicePanel.GetCurrentIcon();
        if (activeEmergencyIcon == this)
        {
            _chooseServicePanel.Deactivate();
        }
        Died?.Invoke();
        Destroy(transform.parent.gameObject);
    }

    private IEnumerator ParticleRoutine()
    {
        yield return new WaitForSeconds(_particleSystemLifeTime);
        Died?.Invoke();
        Destroy(transform.parent.gameObject);
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
