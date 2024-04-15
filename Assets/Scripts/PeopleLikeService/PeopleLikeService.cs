using System;
using UnityEngine;
using Zenject;

public class PeopleLikeService : MonoBehaviour
{
    public event Action OnZeroLikes;
    public event Action OnLikesChanged;
    public int CurrentLikes => _currentLikes;

    [SerializeField] private int _maxLikes;

    [Inject] private ChooseServicePanel _chooseServicePanel;

    private int _currentLikes;

    private void Awake()
    {
        _currentLikes = _maxLikes;
    }

    private void DecreaseLike()
    {
        if (_currentLikes <= 0)
            return;

        _currentLikes--;
        OnLikesChanged?.Invoke();

        if (_currentLikes == 0)
        {
            OnZeroLikes?.Invoke();
            Debug.Log("q");
        }
    }

    private void OnEnable()
    {
        _chooseServicePanel.EmergencyFailed += DecreaseLike;
    }

    private void OnDisable()
    {
        _chooseServicePanel.EmergencyFailed -= DecreaseLike;
    }
}
