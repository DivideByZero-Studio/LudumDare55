using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] Camera _gameCamera;
    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromInstance(_gameCamera).AsSingle();
    }
}