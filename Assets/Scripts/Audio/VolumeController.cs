using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private Slider _slider;

    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private VolumeParameter _volumeGroup;

    private const float _multiplier = 80f;
    
    private enum VolumeParameter
    {
        MusicVolume,
        SFXVolume
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(HandleSliderVolumeChanged);
    }

    private void HandleSliderVolumeChanged(float value)
    {
        var volumeValue = value * _multiplier - _multiplier;
        _mixer.SetFloat(_volumeGroup.ToString(), volumeValue);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(HandleSliderVolumeChanged);
    }
}
