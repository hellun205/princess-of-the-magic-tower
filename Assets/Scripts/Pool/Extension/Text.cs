using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pool.Extension
{
  public class Text : UsePool
  {
    public TextMeshProUGUI tmp;
    public Image background;

    [SerializeField]
    private Text original;

    public void SetText(object value, Color? bg = null, Color? fg = null)
    {
      tmp.text = value.ToString();
      if (bg.HasValue) background.color = bg.Value;
      if (fg.HasValue) tmp.color = fg.Value;
    }

    protected override void OnSummon()
    {
      tmp.text = original.tmp.text;
      tmp.color = original.tmp.color;
      tmp.fontSize = original.tmp.fontSize;
      tmp.fontWeight = original.tmp.fontWeight;
      background.color = original.background.color;
    }
  }
}
