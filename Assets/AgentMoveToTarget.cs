using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMoveToTarget : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform target;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _agent.SetDestination(target.position);
    }
}
