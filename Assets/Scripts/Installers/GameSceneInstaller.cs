using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] Camera _gameCamera;
    [SerializeField] ChooseServicePanel _chooseServicePanel;
    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromInstance(_gameCamera).AsSingle();
        Container.Bind<ChooseServicePanel>().FromInstance(_chooseServicePanel).AsSingle();
    }
}