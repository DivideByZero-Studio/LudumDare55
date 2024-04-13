using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLife : MonoBehaviour
{
    [SerializeField] private float _timeToDespawn;

    private void Start()
    {
        StartCoroutine(CloudLifeRoutine());
    }

    private IEnumerator CloudLifeRoutine()
    {
        yield return new WaitForSeconds(_timeToDespawn);
    }
}
