using UnityEngine;

public class ChooseServicePanelVisuals : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);    
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
