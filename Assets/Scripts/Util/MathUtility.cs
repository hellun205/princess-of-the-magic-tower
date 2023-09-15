using UnityEngine;

namespace Util
{
  public static class MathUtility
  {
    public static bool Approximately(this float a, float b, float tolerance = 0.1f)
      => Mathf.Abs(a - b) < tolerance;

    public static float EaseIn(this float t, float pow = 2f)
      => Mathf.Pow(t, pow);

    public static float Flip(this float x)
      => 1 - x;

    public static float EaseOut(this float t, float pow = 2f)
      => Flip(Mathf.Pow(Flip(t), pow));

    public static float EaseInOut(this float t, float pow = 2f)
      => Mathf.Lerp(EaseIn(t, pow), EaseOut(t, pow), t);
  }
}