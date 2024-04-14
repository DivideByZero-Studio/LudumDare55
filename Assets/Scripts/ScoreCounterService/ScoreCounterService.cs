using System;
using UnityEngine;
using Zenject;

public class ScoreCounterService : MonoBehaviour
{
    public event Action<int> ScoreChanged; 

    [SerializeField, Min(0)] private int _scoreByEmergencyDone;

    [Inject] private ChooseServicePanel _chooseServicePanel;

    private int _score = 0;

    private void AddScoreByDoneEmergency(Transform emergencyTransform, EmergencyType emergencyType)
    {
        _score += _scoreByEmergencyDone;
        ScoreChanged?.Invoke(_score);
    }

    private void OnEnable()
    {
        _chooseServicePanel.EmergencyDone += AddScoreByDoneEmergency;
    }

    private void OnDisable()
    {
        _chooseServicePanel.EmergencyDone -= AddScoreByDoneEmergency;
    }
}
