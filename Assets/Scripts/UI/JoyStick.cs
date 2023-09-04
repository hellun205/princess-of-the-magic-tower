using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Util;

namespace UI
{
  public class JoyStick : UIMonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
  {
    [SerializeField]
    private Image bg;

    [SerializeField]
    private Image circle;

    [SerializeField]
    private float maxRange;

    public delegate void JoyStickEventListener(Vector2 value);

    public event JoyStickEventListener onValueChange;

    private Vector2 position
    {
      get => circle.rectTransform.anchoredPosition;
      set
      {
        circle.rectTransform.anchoredPosition = value;
        onValueChange?.Invoke(this.value);
      }
    }

    public Vector2 value
    {
      get => new Vector2(position.x / maxRange, position.y / maxRange);
      set => position = new Vector2(value.x * maxRange, value.y * maxRange);
    }

    [NonSerialized]
    public bool isUsing;

    public void OnPointerDown(PointerEventData eventData)
    {
      isUsing = true;
      Move(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (!isUsing) return;
      Move(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      isUsing = false;
      position = Vector2.zero;
    }

    private void Move(PointerEventData eventData)
    {
      var inputDir = eventData.position - rectTransform.anchoredPosition;
      var clampedDir = inputDir.magnitude < maxRange ? inputDir : inputDir.normalized * maxRange;

      position = clampedDir;
    }
  }
}
