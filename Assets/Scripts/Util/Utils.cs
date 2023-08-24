using UnityEngine;
using System.Collections;

namespace Util
{
  public static class Utils
  {
    public delegate T Getter<T>() where T : struct;
    public delegate void Setter<T>(T value) where T : struct;
    public delegate bool Equal<T>(T a, T b) where T : struct;
    public delegate T Lerp<T>(T a, T b, float t) where T : struct;

    public static IEnumerator ChangeSmooth<T>
    (
      Getter<T> getter,
      Setter<T> setter,
      T value,
      float smooth,
      Lerp<T> lerp,
      Equal<T> equal
    ) where T : struct {
      while (equal(getter(), value)) {
        setter(lerp(getter(), value, Time.deltaTime * smooth));

        yield return new WaitForEndOfFrame();
      }
    }
  }
}