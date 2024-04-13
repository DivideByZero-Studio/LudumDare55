using System;
using System.Collections;
using UnityEngine;

public class MaterialDissolve : MonoBehaviour
{
    [SerializeField] private float timeToDissolve;

    private readonly int materialDissolve = Shader.PropertyToID("_Dissolve");

    private Material material;

    public event Action DissolveEnded;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    public void StartDissolve()
    {
        StartCoroutine(DissolveRoutine());
    }

    private IEnumerator DissolveRoutine()
    {
        float time = timeToDissolve;
        float step = 1f / timeToDissolve;
        float progress = 0f;

        while (time > 0) 
        {
            progress += step * Time.deltaTime;
            material.SetFloat(materialDissolve, progress);
            time -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        DissolveEnded?.Invoke();
    }
}
