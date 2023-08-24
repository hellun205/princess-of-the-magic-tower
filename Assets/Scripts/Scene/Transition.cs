using Managers;
using System;
using UnityEngine;

namespace Scene
{
  public class Transition : MonoBehaviourSingleton<Transition>
  {
    public Animator targetImg;

    private Action callback;

    protected override void Awake() {
      base.Awake();

      targetImg = GameObject.Find("@transition").GetComponent<Animator>();
    }

    public void Play(string type, float speed, Action callback = null) {
      this.callback = callback;
      targetImg.SetFloat("speed", 1 / speed);
      targetImg.Play(type);
    }

    public void Callback() => callback?.Invoke();
  }

  public static class Transitions
  {
    public const string FADEIN = "FadeIn";
    public const string IN = "FadeIn";
    public const string FADEOUT = "FadeOut";
    public const string OUT = "Out";
  }
}