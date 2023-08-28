using Managers;
using UnityEngine;
using Util;

namespace Pool
{
  public class UIPoolObject : PoolObject
  {
    private Vector2 _position;
    private RectTransform rectTransform;

    private void Awake()
    {
      rectTransform = GetComponent<RectTransform>();
    }

    public override Vector2 position
    {
      get => _position;
      set
      {
        _position = value;
        rectTransform.anchoredPosition =
          GameManager.Pool.uiParent.GetComponent<RectTransform>().WorldToScreenSpace(_position);
      }
    }
  }
}