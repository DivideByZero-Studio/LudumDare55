using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private Slider _slider;

    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private VolumeParameter _volumeGroup;

    private const float _multiplier = 20f;
    
    private enum VolumeParameter
    {
        MusicVolume,
        SFXVolume
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(HandleSliderVolumeChanged);
        _mixer.GetFloat(_volumeGroup.ToString(), out float value);
        _slider.value = (value + 80f) / 80f;
    }

    private void HandleSliderVolumeChanged(float value)
    {
        var volumeValue = Mathf.Log10(value) * _multiplier;
        if (volumeValue < -80f) volumeValue = -80f;
        _mixer.SetFloat(_volumeGroup.ToString(), volumeValue);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(HandleSliderVolumeChanged);
    }
}
