using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] Camera _gameCamera;
    [SerializeField] ChooseServicePanel _chooseServicePanel;
    [SerializeField] TimeCountService _timeCountService;
    [SerializeField] EmergencySpawnService _emergencySpawnService;
    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromInstance(_gameCamera).AsSingle();
        Container.Bind<ChooseServicePanel>().FromInstance(_chooseServicePanel).AsSingle();
        Container.Bind<TimeCountService>().FromInstance(_timeCountService).AsSingle();
        Container.Bind<EmergencySpawnService>().FromInstance(_emergencySpawnService).AsSingle();
    }
}