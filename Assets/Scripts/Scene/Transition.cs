using System;
using UnityEngine;

namespace Scene
{
  public class Transition
  {
    public Animator targetImg = GameObject.Find("@transition").GetComponent<Animator>();

    private Action callback;

    /// <summary>
    /// 전환 애니메이션을 실행합니다.
    /// </summary>
    /// <param name="type">전환 애니메이션 타입</param>
    /// <param name="speed">속도</param>
    /// <param name="callback">전환이 완료된 후 콜백 함수</param>
    public void Play(string type, float speed = 1f, Action callback = null)
    {
      this.callback = callback;
      targetImg.SetFloat("speed", speed == 0f ? 1f : 1f / speed);
      targetImg.Play(type);
    }

    public void Callback() => callback?.Invoke();
  }

  public static class Transitions
  {
    public enum Direction
    {
      Left,
      Right,
      Bottom,
      Top
    }

    public static Direction ToReverse(this Direction dir)
      => dir switch
      {
        Direction.Left => Direction.Right,
        Direction.Right => Direction.Left,
        Direction.Bottom => Direction.Top,
        Direction.Top => Direction.Bottom,
        _ => throw new NotImplementedException()
      };

    public static char GetInitial(this Direction dir) => dir.ToString()[0];

    public const string FADEIN = "FadeIn";
    public const string IN = "In";
    public const string FADEOUT = "FadeOut";
    public const string OUT = "Out";
    public static string PUSHIN(Direction start) 
      => $"Push{start.GetInitial()}2{start.ToReverse().GetInitial()}In";
    
    public static string PUSHOUT(Direction start) 
      => $"Push{start.GetInitial()}2{start.ToReverse().GetInitial()}Out";
  }
}