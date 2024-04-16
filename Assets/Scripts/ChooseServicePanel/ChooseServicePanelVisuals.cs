using UnityEngine;

public class ChooseServicePanelVisuals : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        _animator.Play("ActivatePanel");
    }

    public void Deactivate()
    {
        _animator.Play("DeactivatePanel");
    }
}
