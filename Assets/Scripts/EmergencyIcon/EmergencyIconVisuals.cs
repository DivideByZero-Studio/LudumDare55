using UnityEngine;
using Zenject;

[RequireComponent(typeof(ClickReciever))]
public class EmergencyIconVisuals : MonoBehaviour
{
    [Inject] private Camera _camera;

    [SerializeField] private Animator _animator;

    public void ZoomIn()
    {
        _animator.SetBool("Zoomed", true);
    }

    public void ZoomOut()
    {
        _animator.SetBool("Zoomed", false);
    }
 
    private void OnEnable()
    {
        transform.LookAt(_camera.transform);
    }
}
