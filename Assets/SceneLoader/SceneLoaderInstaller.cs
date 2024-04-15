using System;
using UnityEngine;
using Zenject;

public class SceneLoaderInstaller : MonoInstaller
{
    [SerializeField] private SceneLoaderService m_loaderServicePrefab;

    public override void InstallBindings()
    {
        var loaderServiceInstance = Container.InstantiatePrefabForComponent<SceneLoaderService>(m_loaderServicePrefab);
        Container.Bind<SceneLoaderService>().FromInstance(loaderServiceInstance).AsSingle();
    }
}
