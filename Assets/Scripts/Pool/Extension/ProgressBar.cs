using UnityEngine;

namespace Pool.Extension
{
  [RequireComponent(typeof(UI.ProgressBar))]
  public class ProgressBar : UsePool
  {
    public UI.ProgressBar progressBar;

    protected override void Awake()
    {
      base.Awake();
      progressBar = GetComponent<UI.ProgressBar>();
    }

    protected override void OnKill()
    {
      progressBar.value = 0f;
      progressBar.maxValue = 0f;
    }
  }
}
