using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Util
{
  [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
  public class UIVisibler : MonoBehaviour
  {
    [SerializeField]
    private CanvasGroup canvasGroup;

    private Timer timer = new Timer();
    private int end;
    private int start => end == 1 ? 0 : 1;

    public bool isVisible { get; private set; } = true;


    private void Reset()
    {
      canvasGroup = GetComponent<CanvasGroup>();
      SetVisible(true);
    }

    private void Awake()
    {
      timer.isUnscaled = true;
      timer.onTick += TimerOnTick;
    }

    private void TimerOnTick(Timer sender)
    {
      canvasGroup.alpha = Mathf.Lerp(start, end, sender.value);
    }

    public void SetVisible
    (
      bool visible,
      float? duration = null,
      TimerType type = TimerType.Normal,
      bool ignoreEqual = false
    )
    {
      if (ignoreEqual && isVisible == visible) return;
      isVisible = visible;
      
      canvasGroup.interactable = visible;
      canvasGroup.blocksRaycasts = visible;

      if (duration.HasValue)
      {
        timer.duration = duration.Value;
        timer.type = type;
        end = visible ? 1 : 0;
        timer.Start();
      }
      else
        canvasGroup.alpha = visible ? 1f : 0f;
    }
  }

  public static class UIVisiblerUtility
  {
    public static void SetVisible
    (
      this Object obj,
      bool visible,
      float? duration = null,
      TimerType type = TimerType.Normal,
      bool ignoreEqual = false
    )
      => obj.GetComponent<UIVisibler>().SetVisible(visible, duration, type, ignoreEqual);

    public static bool IsVisible(this Object obj) => obj.GetComponent<UIVisibler>().isVisible;
  }
}
