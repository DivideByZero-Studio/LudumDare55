using UnityEngine;
using Zenject;

public class MainMenuService : MonoBehaviour
{
    [SerializeField] private Animator _cameraAnimator;

    [Inject] private SceneLoaderService _sceneLoaderService;
    public void SwitchToGameScene()
    {
        _sceneLoaderService.LoadScene("SampleScene");
    }

    public void SwitchToSettings()
    {
        _cameraAnimator.Play("SwitchToSettings");
    }

    public void SwitchToMainFromSettings()
    {
        _cameraAnimator.Play("SwitchToMainFromSettings");
    }

    public void SwitchToAboutUs()
    {
        _cameraAnimator.Play("SwitchToAboutUs");
    }

    public void SwitchToMainFromAboutUs()
    {
        _cameraAnimator.Play("SwitchToMainFromAboutUs");
    }
}
