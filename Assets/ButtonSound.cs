using UnityEngine;
using Zenject;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    [Inject] private AudioService _audioService;

    public void PlayButtonSFX()
    {
        _audioService.PlaySFX(_clip, 1.5f);
    }
}
