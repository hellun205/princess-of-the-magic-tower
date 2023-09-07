using Managers;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;
using static Util.Utils;

namespace Scene
{
  public class SceneController : MonoBehaviour
  {
    private Action callback;
    public bool isLoading;
    private Coroutiner<string, TransitionOption, TransitionOption, bool> loadCrt;

    private void Awake()
    {
      loadCrt = new(LoadRoutine);
    }

    /// <summary>
    /// <inheritdoc cref="Load(string,System.Action,bool)"/>>
    /// </summary>
    /// <param name="sceneName">로드 할 Scene 이름</param>
    /// <param name="callback">로드 된 후 콜백 함수</param>
    /// <param name="smoothPause">timescale을 서서히 줄임</param>
    public void Load(string sceneName, Action callback = null, bool smoothPause = true)
      => Load(sceneName, Transitions.OUT, Transitions.IN, callback, smoothPause);

    /// <summary>
    /// 해당 Scene을 로드합니다.
    /// </summary>
    /// <param name="sceneName">로드 할 Scene 이름</param>
    /// <param name="outAnimation">Out 애니메이션</param>
    /// <param name="inAnimation">In 애니메이션</param>
    /// <param name="callback">로드 된 후 콜백 함수</param>
    /// <param name="smoothPause">timescale을 서서히 줄임</param>
    public void Load
    (
      string sceneName,
      TransitionOption outAnimation,
      TransitionOption inAnimation,
      Action callback = null,
      bool smoothPause = true
    )
    {
      if (isLoading)
      {
        Debug.LogError("already loading scene.");
        return;
      }

      this.callback = callback;
      loadCrt.Start(sceneName, outAnimation, inAnimation, smoothPause);
    }

    private IEnumerator LoadRoutine(string sceneName, TransitionOption outAnimation, TransitionOption inAnimation,
      bool smoothPause)
    {
      isLoading = true;
      var load = SceneManager.LoadSceneAsync(sceneName);
      load.allowSceneActivation = false;

      yield return new WaitForSecondsRealtime(outAnimation.delay);
      Pause(smoothPause);

      GameManager.Transition.Play(outAnimation.type, outAnimation.speed);

      yield return new WaitForSecondsRealtime(outAnimation.speed);
      yield return new WaitUntil(() => load.progress >= 0.9f);

      load.allowSceneActivation = true;

      yield return new WaitForSecondsRealtime(inAnimation.delay);
      GameManager.Transition.Play(inAnimation.type, inAnimation.speed);

      UnPause(smoothPause);
      yield return new WaitForSecondsRealtime(inAnimation.speed);

      isLoading = false;
      callback?.Invoke();
    }
  }
}