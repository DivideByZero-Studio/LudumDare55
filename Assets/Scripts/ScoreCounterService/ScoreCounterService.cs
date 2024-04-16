using System;
using UnityEngine;
using Zenject;

public class ScoreCounterService : MonoBehaviour
{
    public event Action<int> ScoreChanged; 

    [SerializeField, Min(0)] private int _scoreByEmergencyDone;

    [Inject] private ChooseServicePanel _chooseServicePanel;

    public int Score { get; private set; }

    private void AddScoreByDoneEmergency(Transform emergencyTransform, EmergencyType emergencyType)
    {
        Score += _scoreByEmergencyDone;
        ScoreChanged?.Invoke(Score);
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
