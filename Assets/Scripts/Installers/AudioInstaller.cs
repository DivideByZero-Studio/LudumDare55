using UnityEngine;
using Zenject;

public class AudioInstaller : MonoInstaller
{
    [SerializeField] private AudioService audioServicePrefab;

    public override void InstallBindings()
    {
        var audioServiceInstance = Container.InstantiatePrefabForComponent<AudioService>(audioServicePrefab);
        Container.Bind<AudioService>().FromInstance(audioServiceInstance).AsSingle();
    }
}
