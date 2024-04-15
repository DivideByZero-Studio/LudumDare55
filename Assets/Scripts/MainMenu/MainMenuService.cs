using UnityEngine;
using Zenject;

public class MainMenuService : MonoBehaviour
{
    [SerializeField] private Animator _cameraAnimator;

    public void SwitchToGameScene()
    {
        Debug.Log("Game Scene");
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
