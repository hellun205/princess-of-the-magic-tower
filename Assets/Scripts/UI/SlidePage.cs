using System;
using UnityEngine;
using Util;

namespace UI
{
  public class SlidePage : MonoBehaviour
  {
    [SerializeField]
    private float width;

    public int index { get; set; }
    
    public bool isUnscaled;

    public float smoothing = 5f;

    [SerializeField]
    private RectTransform movingObject;

    private void Awake()
    {
    }

    private void Update()
    {
      var pos = movingObject.anchoredPosition;
      movingObject.anchoredPosition = Vector2.Lerp
      (
        pos,
        new Vector2(-width * index, pos.y),
        (isUnscaled ? Time.unscaledDeltaTime : Time.deltaTime) * smoothing
      );
    }
  }
}
