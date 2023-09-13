using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
  public class Touch :
    Selectable,
    IPointerClickHandler,
    IDragHandler,
    IBeginDragHandler,
    IEndDragHandler,
    IDropHandler,
    IPointerMoveHandler
  {
    public delegate void PointerEventListener(PointerEventData eventData);

    public PointerEventListener onTouch;
    public PointerEventListener onTouchDown;
    public PointerEventListener onTouchUp;
    public PointerEventListener onTouchMove;
    public PointerEventListener onPointerEnter;
    public PointerEventListener onPointerExit;
    public PointerEventListener onDrag;
    public PointerEventListener onBeginDrag;
    public PointerEventListener onEndDrag;
    public PointerEventListener onDrop;

    public void OnPointerClick(PointerEventData eventData)
    {
      if (interactable) onTouch?.Invoke(eventData);
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
      base.OnPointerDown(eventData);
      if (interactable) onTouchDown?.Invoke(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
      base.OnPointerEnter(eventData);
      if (interactable) onPointerEnter?.Invoke(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
      base.OnPointerExit(eventData);
      if (interactable) onPointerExit?.Invoke(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
      base.OnPointerUp(eventData);
      if (interactable) onTouchUp?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (interactable) onDrag?.Invoke(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      if (interactable) onBeginDrag?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      if (interactable) onEndDrag?.Invoke(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
      if (interactable) onDrop?.Invoke(eventData);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
      if (interactable) onTouchMove?.Invoke(eventData);
    }
  }
}