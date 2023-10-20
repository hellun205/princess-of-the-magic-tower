using System.Linq;
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
      Clear();

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
        Debug.Log(childs[i]);
        childs[i].color = i < index ? enabledColor : disabledColor;
      }
    }

    private Image CreateBlock()
    {
      var go = new GameObject("block", typeof(RectTransform), typeof(Image), typeof(Outline));

      var img = go.GetComponent<Image>();
      var outline = go.GetComponent<Outline>();

      outline.effectColor = Color.black;
      outline.effectDistance = new Vector2(0.5f, 0.5f);
      go.transform.SetParent(transform);
      go.transform.localScale = Vector3.one;
      go.transform.position = go.transform.position.Setter(z: transform.position.z);
      this.RebuildLayout();

      return img;
    }

    protected override void OnKill()
    {
      
    }

    private void Clear()
    {
      var childs = transform.GetChilds<Image>();

      foreach (var child in childs)
        Destroy(child.gameObject);
    }
  }
}
