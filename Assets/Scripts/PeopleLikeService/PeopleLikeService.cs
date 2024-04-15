using System;
using UnityEngine;
using Zenject;

public class PeopleLikeService : MonoBehaviour
{
    public event Action OnZeroLikes;
    public event Action OnLikesChanged;
    public int CurrentLikes => _currentLikes;

    [SerializeField] private int _maxLikes;
    [SerializeField] private int _addCountLikes;
    [SerializeField] private int _decreaseCountLikes;

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

        _currentLikes = Math.Clamp(_currentLikes - _decreaseCountLikes, 0, _maxLikes);
        OnLikesChanged?.Invoke();

        if (_currentLikes == 0)
            OnZeroLikes?.Invoke();
    }

    private void AddLike(Transform iconTransform, EmergencyType type)
    {
        if (_currentLikes == _maxLikes)
            return;

        _currentLikes = Math.Clamp(_currentLikes + _addCountLikes, 0, _maxLikes);
        OnLikesChanged?.Invoke();
    }

    private void OnEnable()
    {
        _chooseServicePanel.EmergencyFailed += DecreaseLike;
        _chooseServicePanel.EmergencyDone += AddLike;
    }

    private void OnDisable()
    {
        _chooseServicePanel.EmergencyFailed -= DecreaseLike;
        _chooseServicePanel.EmergencyDone -= AddLike;
    }
}
