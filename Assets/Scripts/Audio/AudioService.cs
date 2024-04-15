using UnityEngine;

public class AudioService : MonoBehaviour
{
    [Header("Audio sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundFXSource;
    [SerializeField] private AudioSource _ambientSource;

    public void PlayMusic(AudioClip clip)
    {
        if (_musicSource.clip == clip) return;

        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void PlayMusicForced(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        Debug.Log("hui tebe");
        _musicSource.clip = null;
        _musicSource.loop = false;
        _musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        _soundFXSource.volume = volume;
        _soundFXSource.PlayOneShot(clip);
    }

    public void PlayAmbient(AudioClip clip, float volume = 1f)
    {
        _soundFXSource.volume = volume;
        _soundFXSource.PlayOneShot(clip);
    }
}