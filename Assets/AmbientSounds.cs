using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AmbientSounds : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _ambientSoundsList;
    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;

    [Inject] private AudioService _audioService;

    private System.Random _random;

    private void Awake()
    {
        _random = new();
    }

    private void Start()
    {
        StartCoroutine(AmbientSoundsRoutine());
    }

    private IEnumerator AmbientSoundsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds((float)_random.NextDouble() * (_maxTime - _minTime) + _minTime);
            _audioService.PlayAmbient(_ambientSoundsList[_random.Next(_ambientSoundsList.Count)]);
        }
    }

}
