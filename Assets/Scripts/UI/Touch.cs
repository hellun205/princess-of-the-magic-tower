using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
  public class Touch :
    MonoBehaviour,
    IPointerClickHandler,
    IPointerUpHandler,
    IPointerDownHandler,
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
    public PointerEventListener onDrag;
    public PointerEventListener onBeginDrag;
    public PointerEventListener onEndDrag;
    public PointerEventListener onDrop;

    public void OnPointerClick(PointerEventData eventData) => onTouch?.Invoke(eventData);
    public void OnPointerUp(PointerEventData eventData) => onTouchUp?.Invoke(eventData);
    public void OnPointerDown(PointerEventData eventData) => onTouchDown?.Invoke(eventData);
    public void OnDrag(PointerEventData eventData) => onDrag?.Invoke(eventData);
    public void OnBeginDrag(PointerEventData eventData) => onBeginDrag?.Invoke(eventData);
    public void OnEndDrag(PointerEventData eventData) => onEndDrag?.Invoke(eventData);
    public void OnDrop(PointerEventData eventData) => onDrop?.Invoke(eventData);
    public void OnPointerMove(PointerEventData eventData) => onTouchMove?.Invoke(eventData);
  }
}