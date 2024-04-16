using UnityEngine;
using Zenject;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private GameObject _screenElements;
    [SerializeField] private AudioClip _loseSound;

    [Inject] private PeopleLikeService _peopleLikeService;
    [Inject] private SceneLoaderService _sceneLoaderService;
    [Inject] private AudioService _audioService;

    private void Start()
    {
        _peopleLikeService.OnZeroLikes += Show;
    }

    private void Show()
    {
        _screenElements.SetActive(true);
        _audioService.StopMusic();
        _audioService.StopAmbient();
        _audioService.PlaySFX(_loseSound);
    }

    public void OnRestartButtonPressed()
    {
        _sceneLoaderService.ReloadScene();
    }

    public void OnMenuButtonPressed()
    {
        _sceneLoaderService.LoadScene("MainMenu");
    }
}
