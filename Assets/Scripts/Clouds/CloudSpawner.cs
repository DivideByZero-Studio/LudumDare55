using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cloudPrefabs;
    [SerializeField] private float _minTimeToSpawn;
    [SerializeField] private float _maxTimeToSpawn;
    private System.Random _random;

    private void Awake()
    {
        _random = new();
    }

    private void Start()
    {
        StartCoroutine(CloudSpawnRoutine());
    }

    private IEnumerator CloudSpawnRoutine()
    {
        yield return new WaitForSeconds((float)_random.NextDouble() * (_maxTimeToSpawn - _minTimeToSpawn) + _minTimeToSpawn);
        while (true)
        {
            Instantiate(_cloudPrefabs[_random.Next(_cloudPrefabs.Count - 1)], transform);
            yield return new WaitForSeconds((float)_random.NextDouble() * (_maxTimeToSpawn - _minTimeToSpawn) + _minTimeToSpawn);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
