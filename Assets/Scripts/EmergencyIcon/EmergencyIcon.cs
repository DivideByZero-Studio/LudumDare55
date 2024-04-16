using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ClickReciever), typeof(BaseEmergency))]
public class EmergencyIcon : MonoBehaviour
{
    public event Action Died;

    public GameObject EmergencyPointPrefab => _emergencyPointPrefab;
    public Transform EmergencyPointTransform => _emergencyPointTransform;

    [SerializeField] private AudioClip _chooseClip;
    [SerializeField] private AudioClip _failClip;
    [SerializeField] private AudioClip _doneClip;

    [SerializeField] private float _maxLifeTime;
    [SerializeField] private float _particleSystemLifeTime;
    [SerializeField] private GameObject _emergencyPointPrefab;

    private Transform _emergencyPointTransform;
    private ClickReciever _clickReciever;
    private BaseEmergency _emergency;
    private EmergencyIconVisuals _visuals;

    [Inject] ChooseServicePanel _chooseServicePanel;
    [Inject] EmergencySpawnService _emergencySpawnService;
    [Inject] TimeCountService _timeCountService;
    [Inject] AudioService _audioService;

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
        _visuals.PlayDoneEffect();
        _audioService.PlaySFX(_doneClip);
        //Play Done effect
    }

    public void PlayFailedEffect()
    {
        StopAllCoroutines();
        StartCoroutine(ParticleRoutine());
        _visuals.Deactivate();
        _visuals.PlayFailedEffect();
        _audioService.PlaySFX(_failClip);
        //Play Failed effect
    }

    public EmergencyType GetEmergencyType()
    {
        return _emergency.Type;
    }

    public void ActivateChoosenEffect()
    {
        _visuals.ZoomIn();
        _audioService.PlaySFX(_chooseClip);
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
        _chooseServicePanel.GetEmergencyFail();
        Died?.Invoke();
        PlayFailedEffect();
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
