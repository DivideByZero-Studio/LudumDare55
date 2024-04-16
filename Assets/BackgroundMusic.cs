using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _music;

    [Inject] private AudioService _audioService;

    void Start()
    {
        _audioService.PlayMusicForced(_music);
    }
}
