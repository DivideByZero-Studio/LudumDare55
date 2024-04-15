using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class EmergencyCar : MonoBehaviour
{
    [SerializeField] private float m_DistanceToDissolve;
    [SerializeField] private MaterialDissolve m_MaterialDissolve;
    [SerializeField] private AudioClip m_SirenSound;

    private NavMeshAgent _agent;
    private Vector3 _destination;
    private bool _isDissolving;

    [Inject] private AudioService _audioService;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        m_MaterialDissolve.DissolveEnded += OnDissolveEnded;
        _audioService.PlaySFX(m_SirenSound);
    }

    public void Initialize(Vector3 destination)
    {
        _destination = destination;
        _agent.SetDestination(destination);
    }

    private void Update()
    {
        if (_isDissolving) return;

        if (Vector3.Distance(transform.position, _destination) <= m_DistanceToDissolve)
        {
            _isDissolving = true;
            m_MaterialDissolve.StartDissolve();
        }
    }

    private void OnDissolveEnded()
    {
        Destroy(gameObject);
    }
}
