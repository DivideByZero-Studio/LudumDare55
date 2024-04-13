using UnityEngine;
using Zenject;

[RequireComponent(typeof(ClickReciever))]
public class EmergencyIconVisuals : MonoBehaviour
{
    [Inject] private Camera _camera;

    [SerializeField] private Animator _animator;
    
    private ClickReciever _clickReciever;

    private void Awake()
    {
        _clickReciever = GetComponent<ClickReciever>();
    }
    private void ZoomIn()
    {
        _animator.SetBool("Zoomed", true);
    }
 
    private void OnEnable()
    {
        _clickReciever.Clicked += ZoomIn;
        transform.LookAt(_camera.transform);
    }

    private void OnDisable()
    {
        _clickReciever.Clicked -= ZoomIn;
    }

}
