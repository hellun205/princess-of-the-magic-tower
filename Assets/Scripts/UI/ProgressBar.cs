using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class ProgressBar : MonoBehaviour
  {
    [Header("Child Objects")]
    [SerializeField]
    private Image yellow;
    
    [SerializeField]
    private Image green;
    
    [SerializeField]
    private Image red;

    [SerializeField]
    private TextMeshProUGUI text;

    [Header("Progress Bar")]
    [SerializeField]
    private float smoothing = 5f;

    [Min(0)]
    public float value;

    [Min(0)]
    public float maxValue;

    public bool isText;

    private void Update()
    {
      var val = maxValue == 0 ? 0 : value / maxValue;
      if (val > red.fillAmount)
      {
        green.fillAmount = val;
        yellow.fillAmount = val;
        red.fillAmount = Mathf.Lerp(red.fillAmount, green.fillAmount, Time.unscaledDeltaTime * smoothing);
      }
      else
      {
        red.fillAmount = val;
        green.fillAmount = val;
        yellow.fillAmount = Mathf.Lerp(yellow.fillAmount, red.fillAmount, Time.unscaledDeltaTime * smoothing);
      }
      text.text = isText ? $"{Mathf.RoundToInt(value)} / {Mathf.RoundToInt(maxValue)}" : "";
    }
  }
}
