using System;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Pool.Extension
{
  public class DividedProgressBar : UsePool
  {
    public readonly Color enabledColor = new Color32(254, 35, 31, 255);
    public readonly Color disabledColor = new Color32(175, 171, 171, 255);

    private int _value;
    private int _maxValue;

    public int value
    {
      get => _value;
      set
      {
        _value = value; 
        SetBlockEnabled(_value);
      }
    }

    public int maxValue
    {
      get => _maxValue;
      set
      {
        _maxValue = value;
        SetBlockCount(_maxValue, _value);
      }
    }

    private void SetBlockCount(int count, int enabledCount = int.MaxValue)
    {
      var childs = transform.GetChilds<Image>();

      foreach (var child in childs)
        Destroy(child);

      for (var i = 0; i < count; i++)
      {
        var block = CreateBlock();
        block.color = i < enabledCount ? enabledColor : disabledColor;
      }
    }

    private void SetBlockEnabled(int index)
    {
      var childs = transform.GetChilds<Image>();

      for (var i = 0; i < childs.Length; i++)
      {
        childs[i].color = i < index ? enabledColor : disabledColor;
      }
    }

    private Image CreateBlock()
    {
      var go = new GameObject("block", typeof(RectTransform), typeof(Image), typeof(Outline), typeof(CanvasRenderer));

      var img = go.GetComponent<Image>();
      var outline = go.GetComponent<Outline>();

      outline.effectDistance = new Vector2(0.5f, 0.5f);
      go.transform.SetParent(transform);
      this.RebuildLayout();

      return img;
    }
  }
}
