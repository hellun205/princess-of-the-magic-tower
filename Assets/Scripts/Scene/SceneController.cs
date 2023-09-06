using Managers;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;
using static Util.Utils;

namespace Scene
{
  public class SceneController : MonoBehaviourSingleton<SceneController>
  {
    private Action callback;
    public bool isLoading;
    private Coroutiner<float, float> timeScaleSmoothCrt;
    private Coroutiner<string, TransitionOption, TransitionOption, bool> loadCrt;

    protected override void Awake()
    {
      base.Awake();
      timeScaleSmoothCrt = new(TimeScaleSmooth);
      loadCrt = new(LoadRoutine);
    }

    public void Load
    (
      string sceneName,
      TransitionOption outAnimation,
      TransitionOption inAnimation,
      Action callback = null,
      bool slowly = true
    )
    {
      if (isLoading)
      {
        Debug.LogError("already loading scene.");
        return;
      }

      this.callback = callback;
      loadCrt.Start(sceneName, outAnimation, inAnimation, slowly);
    }

    private IEnumerator LoadRoutine(string sceneName, TransitionOption outAnimation, TransitionOption inAnimation, bool slowly)
    {
      
      isLoading = true;
      var load = SceneManager.LoadSceneAsync(sceneName);
      load.allowSceneActivation = false;

      yield return new WaitForSecondsRealtime(outAnimation.delay);
      if (slowly)
        timeScaleSmoothCrt.Start(0f, 1f);

      GameManager.Transition.Play(outAnimation.type, outAnimation.speed);

      yield return new WaitForSecondsRealtime(outAnimation.speed);
      yield return new WaitUntil(() => load.progress >= 0.9f);
      
      timeScaleSmoothCrt.Stop();
      
      load.allowSceneActivation = true;

      yield return new WaitForSecondsRealtime(inAnimation.delay);
      GameManager.Transition.Play(inAnimation.type, inAnimation.speed);
      if (slowly)
        timeScaleSmoothCrt.Start(1f, 3f);
      yield return new WaitForSecondsRealtime(inAnimation.speed);

      isLoading = false;
      callback?.Invoke();
    }

    private IEnumerator TimeScaleSmooth(float value, float smoothing = 1f)
    {
      while (!Time.timeScale.Approximately(value, 0.2f))
      {
        Time.timeScale = Mathf.Lerp(Time.timeScale, value, Time.unscaledDeltaTime * smoothing);

        yield return new WaitForEndOfFrame();
      }

      Time.timeScale = value;
    }
  }
}
