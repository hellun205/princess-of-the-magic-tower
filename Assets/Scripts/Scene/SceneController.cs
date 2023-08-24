using Managers;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Util.Utils;

namespace Scene
{
  public class SceneController : MonoBehaviourSingleton<SceneController>
  {
    private Action callback;
    public bool isLoading;

    public void Load(string sceneName, TransitionOption outAnimation, TransitionOption inAnimation, Action callback = null, bool slowly = true) {
      if (isLoading) {
        Debug.LogError("already loading scene.");
        return;
      }

      this.callback = callback;
      StartCoroutine(LoadRoutine(sceneName, outAnimation, inAnimation, slowly));
    }

    private IEnumerator LoadRoutine(string sceneName, TransitionOption outAnimation, TransitionOption inAnimation, bool slowly) {
      Coroutine crt = null;
      isLoading = true;
      var load = SceneManager.LoadSceneAsync(sceneName);
      load.allowSceneActivation = false;

      yield return new WaitForSecondsRealtime(outAnimation.delay);

      //if (slowly) 
      //  crt = StartCoroutine(ChangeSmooth(() => Time.timeScale, (v) => Time.timeScale = v, 0f, 2f, Mathf.Lerp, Mathf.Approximately));
      
      GameManager.Transition.Play(outAnimation.type, outAnimation.speed);

      yield return new WaitForSecondsRealtime(outAnimation.speed);
      yield return new WaitUntil(() => load.progress >= 0.9f);

      if (slowly && crt != null)
        StopCoroutine(crt);
      load.allowSceneActivation = true;

      yield return new WaitForSecondsRealtime(inAnimation.delay);
      GameManager.Transition.Play(inAnimation.type, inAnimation.speed);
      //if (slowly) 
      //  crt = StartCoroutine(ChangeSmooth(() => Time.timeScale, (v) => Time.timeScale = v, 1f, 2f, Mathf.Lerp, Mathf.Approximately));

      yield return new WaitForSecondsRealtime(inAnimation.speed);

      isLoading = false;
      callback?.Invoke();
    }

    private IEnumerator TimeScaleSmooth(float value, float smoothing = 5f) {
      while(Mathf.Approximately(Time.timeScale, value)) {
        Time.timeScale = Mathf.Lerp(Time.timeScale, value, Time.deltaTime * smoothing);

        yield return new WaitForEndOfFrame();
      }
    }


  }
}
