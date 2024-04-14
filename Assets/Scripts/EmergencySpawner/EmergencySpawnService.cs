using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EmergencySpawnService : MonoBehaviour
{
    public AnimationCurve _difficultyMultiplier;
    [SerializeField] private float _timeToSpawn;
    [SerializeField] private List<EmergencySpawner> _spawners;

    [Inject] private TimeCountService _timeCountService;

    private System.Random _random;

    private void Awake()
    {
        _random = new System.Random();
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());   
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            var spawner = GetRandomSpawner();
            var type = GetRandomType();

            if (spawner != null)
                spawner.SpawnByType(type);

            Debug.Log(_timeToSpawn / (1f / _difficultyMultiplier.Evaluate(_timeCountService.TimePassed)));
            yield return new WaitForSeconds(_timeToSpawn / _difficultyMultiplier.Evaluate(_timeCountService.TimePassed));
        }
    }


    private EmergencyType GetRandomType()
    {
        int index = _random.Next(0, 3);
        switch (index)
        {
            case 0:
                return EmergencyType.Fire;
            case 1:
                return EmergencyType.Ambulance;
            case 2:
                return EmergencyType.Police;
        }
        return EmergencyType.Ambulance;
    }

    private EmergencySpawner GetRandomSpawner()
    {
        List<EmergencySpawner> nonOccupiedSpawners = GetNonOccupiedSpawners();
        if (nonOccupiedSpawners.Count == 0)
            return null;

        var spawner = nonOccupiedSpawners[_random.Next(nonOccupiedSpawners.Count - 1)];
        return spawner;
    }

    private List<EmergencySpawner> GetNonOccupiedSpawners()
    {
        List<EmergencySpawner> spawners = new List<EmergencySpawner>();
        foreach(var spawner in _spawners)
        {
            if (spawner.Occupied == false) spawners.Add(spawner);
        }
        return spawners;
    }
}
