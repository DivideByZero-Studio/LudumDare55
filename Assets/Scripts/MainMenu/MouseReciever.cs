using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseReciever : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UnityEvent Clicked;

    [SerializeField] private Canvas _textCanvas;

    private Outline _outline; 

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _textCanvas.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _outline.enabled = true;
        _textCanvas.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _outline.enabled = false;
        _textCanvas.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke();
        _textCanvas.gameObject.SetActive(false);
    }
}
