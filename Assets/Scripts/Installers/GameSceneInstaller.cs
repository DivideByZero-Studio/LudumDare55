using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Camera _gameCamera;
    [SerializeField] private ChooseServicePanel _chooseServicePanel;
    [SerializeField] private TimeCountService _timeCountService;
    [SerializeField] private EmergencySpawnService _emergencySpawnService;
    [SerializeField] private ScoreCounterService _scoreCounterService;
    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromInstance(_gameCamera).AsSingle();
        Container.Bind<ChooseServicePanel>().FromInstance(_chooseServicePanel).AsSingle();
        Container.Bind<TimeCountService>().FromInstance(_timeCountService).AsSingle();
        Container.Bind<EmergencySpawnService>().FromInstance(_emergencySpawnService).AsSingle();
        Container.Bind<ScoreCounterService>().FromInstance(_scoreCounterService).AsSingle();
    }
}