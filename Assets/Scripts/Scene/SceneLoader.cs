using Managers;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;
using static Util.Utils;

namespace Scene
{
  public class SceneLoader
  {
    public static bool isLoading;

    private Action callbackOnComplete;

    private Action callbackOnStartIn;

    private Action callbackOnEndIn;

    private Action callbackOnStartOut;

    private Action callbackOnEndOut;

    private Coroutine loadCrt;

    private TransitionOption inTransition = Transitions.IN;

    private TransitionOption outTransition = Transitions.OUT;

    private bool isSmooth = false;

    private bool isPause = false;

    private string sceneName;

    public Coroutine coroutine;

    public SceneLoader(string sceneName)
    {
      this.sceneName = sceneName;
    }

    public SceneLoader In(TransitionOption value)
    {
      inTransition = value;
      return this;
    }

    public SceneLoader In(string type, float speed = 1f, float delay = 0f)
    {
      inTransition = new TransitionOption(type, speed, delay);
      return this;
    }

    public SceneLoader Out(TransitionOption value)
    {
      outTransition = value;
      return this;
    }

    public SceneLoader Out(string type, float speed = 1f, float delay = 0f)
    {
      outTransition = new TransitionOption(type, speed, delay);
      return this;
    }

    public SceneLoader OnComplete(Action value)
    {
      callbackOnComplete = value;
      return this;
    }

    public SceneLoader OnEndIn(Action value)
    {
      callbackOnEndIn = value;
      return this;
    }

    public SceneLoader OnEndOut(Action value)
    {
      callbackOnEndOut = value;
      return this;
    }

    public SceneLoader OnStartIn(Action value)
    {
      callbackOnStartIn = value;
      return this;
    }

    public SceneLoader OnStartOut(Action value)
    {
      callbackOnStartOut = value;
      return this;
    }

    public SceneLoader PauseOnTransitioning(bool smooth = true)
    {
      isPause = true;
      isSmooth = smooth;

      return this;
    }

    public SceneLoader Load()
    {
      if (isLoading) return this;
        // throw new Exception($"already loading scene: {sceneName}");
      coroutine = GameManager.Manager.StartCoroutine(LoadRoutine());
      return this;
    }

    private IEnumerator LoadRoutine()
    {
      isLoading = true;
      var load = SceneManager.LoadSceneAsync(sceneName);
      load.allowSceneActivation = false;

      yield return new WaitForSecondsRealtime(outTransition.delay);
      callbackOnStartOut?.Invoke();
      if (isPause) Pause(isSmooth, outTransition.speed * 0.9f);
      GameManager.Transition.Play(outTransition.type, outTransition.speed);

      yield return new WaitForSecondsRealtime(outTransition.speed);
      callbackOnEndOut?.Invoke();

      yield return new WaitUntil(() => load.progress >= 0.9f);
      load.allowSceneActivation = true;

      yield return new WaitForSecondsRealtime(inTransition.delay);
      callbackOnStartIn?.Invoke();
      if (isPause) UnPause(isSmooth, inTransition.speed * 0.9f);
      GameManager.Transition.Play(inTransition.type, inTransition.speed);

      yield return new WaitForSecondsRealtime(inTransition.speed);
      callbackOnEndIn?.Invoke();

      isLoading = false;
      callbackOnComplete?.Invoke();
    }
  }
}