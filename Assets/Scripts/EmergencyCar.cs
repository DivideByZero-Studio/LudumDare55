using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EmergencyCar : MonoBehaviour
{
    [SerializeField] private float distanceToDissolve;
    [SerializeField] private MaterialDissolve materialDissolve;

    private NavMeshAgent _agent;
    private Vector3 _destination;
    private bool _isDissolving;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        materialDissolve.DissolveEnded += OnDissolveEnded;
    }

    public void Initialize(Vector3 destination)
    {
        _destination = destination;
        _agent.SetDestination(destination);
    }

    private void Update()
    {
        if (_isDissolving) return;

        if (Vector3.Distance(transform.position, _destination) <= distanceToDissolve)
        {
            _isDissolving = true;
            materialDissolve.StartDissolve();
        }
    }

    private void OnDissolveEnded()
    {
        Destroy(gameObject);
    }
}
