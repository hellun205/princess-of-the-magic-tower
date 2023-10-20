using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managers;
using Scene;
using Random = UnityEngine.Random;

namespace Util
{
  public static class Utils
  {
    private static Coroutiner<float, float> smoothTsCrt = new(SetTimeScaleSmooth);

    public static float GetAngleOfLookAtObject(this Transform sender, Transform target)
    {
      var offset = target.transform.position - sender.position;
      return Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
    }

    public static Quaternion GetRotationOfLookAtObject(this Transform sender, Transform target)
    {
      var euler = sender.rotation.eulerAngles;
      return Quaternion.Euler(euler.x, euler.y, GetAngleOfLookAtObject(sender, target));
    }

    /// <summary>
    /// 해당 리스트에서 랜덤으로 하나의 객체를 가져옵니다.
    /// </summary>
    /// <param name="enumerable">리스트</param>
    /// <typeparam name="T">객체 타입</typeparam>
    /// <returns></returns>
    public static T GetRandom<T>(this IEnumerable<T> enumerable)
    {
      var array = enumerable.ToArray();

      return array[Random.Range(0, array.Length)];
    }

    /// <summary>
    /// Color에서 값을 변경하고 반환합니다.
    /// </summary>
    /// <param name="original">원본 Color 값</param>
    /// <param name="r">Red</param>
    /// <param name="g">Green</param>
    /// <param name="b">Blue</param>
    /// <param name="a">Alpha</param>
    /// <returns>값이 변경 된 Color</returns>
    public static Color Setter(this Color original, float? r = null, float? g = null, float? b = null, float? a = null)
    {
      return new Color(r ?? original.r, g ?? original.g, b ?? original.b, a ?? original.a);
    }

    /// <summary>
    /// Vector3에서 값을 변경하고 반환합니다.
    /// </summary>
    /// <param name="original">원본 Vector3 값</param>
    /// <param name="x">X</param>
    /// <param name="y">Y</param>
    /// <param name="z">Z</param>
    /// <returns>값이 변경 된 Vector3</returns>
    public static Vector3 Setter(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
      return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
    }

    /// <summary>
    /// Vector2에서 값을 변경하고 반환합니다.
    /// </summary>
    /// <param name="original">원본 Vector2 값</param>
    /// <param name="x">X</param>
    /// <param name="y">Y</param>
    /// <returns>값이 변경 된 Vector2</returns>
    public static Vector2 Setter(this Vector2 original, float? x = null, float? y = null)
    {
      return new Vector2(x ?? original.x, y ?? original.y);
    }

    /// <summary>
    /// 몇 초 뒤 해당 함수를 실행합니다.
    /// </summary>
    /// <param name="second">초</param>
    /// <param name="fn">함수</param>
    public static Coroutine Wait(float second, Action fn)
      => GameManager.Manager.StartCoroutine(Routine(new WaitForSecondsRealtime(second), fn));

    public static Coroutine WaitUntil(Func<bool> predicate, Action fn)
      => GameManager.Manager.StartCoroutine(Routine(new WaitUntil(predicate), fn));

    public static IEnumerator Routine(CustomYieldInstruction yieldInstruction, Action fn)
    {
      yield return yieldInstruction;
      // try
      // {
        fn.Invoke();
      // }
      // catch (Exception e)
      // {
      //   Debug.Log($"Pass Error: {e.Message}");
      // }
    }

    public static Vector3 WorldToScreenSpace(this RectTransform canvas, Vector3 worldPos)
    {
      var camera = GameManager.Camera.mainCamera;
      var screenPoint = camera.WorldToScreenPoint(worldPos);
      screenPoint.z = 0;

      if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, screenPoint, camera,
            out var screenPos))
        return screenPos;

      return screenPoint;
    }

    public static void For(this int count, Action fn)
    {
      for (var i = 0; i < count; i++) fn?.Invoke();
    }

    public static void For(this int count, Action<int> fn)
    {
      for (var i = 0; i < count; i++) fn?.Invoke(i);
    }

    public static void ExitGame()
    {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public static void Pause(bool smooth = false, float time = 1f)
    {
      smoothTsCrt.Stop();

      if (smooth)
        smoothTsCrt.Start(0f, time);
      else
        Time.timeScale = 0f;
    }

    public static void UnPause(bool smooth = false, float time = 1f)
    {
      smoothTsCrt.Stop();
      if (smooth)
        smoothTsCrt.Start(1f, time);
      else
        Time.timeScale = 1f;
    }

    private static IEnumerator SetTimeScaleSmooth(float value, float time)
    {
      if (Mathf.Approximately(Time.timeScale, value))
        yield break;

      var normalized = Mathf.Abs(Time.timeScale - value);
      var sep = Time.timeScale > value ? -1 : 1;
      while (!Time.timeScale.Approximately(value, 0.07f))
      {
        Time.timeScale += sep * (Time.unscaledDeltaTime * normalized / time);
        yield return new WaitForEndOfFrame();
      }

      Time.timeScale = value;
    }

    public static bool CheckLayer(this int layer, params string[] checks)
      => checks.Any(check => layer == LayerMask.NameToLayer(check));

    public static string ToString<T>(IEnumerable<T> enumerable)
    {
      var sb = new StringBuilder("[");
      foreach (var x in enumerable)
      {
        sb.Append(x)
          .Append(", ");
      }

      sb.Append("]");
      return sb.ToString();
    }

    public static float GetMagnitude(this Vector2 l1, Vector2 l2)
      => (l2 - l1).magnitude;

    public static bool ApplyPercentage(this float percentage, params float[] additive)
    {
      var random = Random.Range(0f, 1f);
      foreach (var add in additive)
        percentage += add;
      return Mathf.Min(1f, Mathf.Max(0, percentage)) > random;
    }

    public static T[] GetChilds<T>(this Transform transform) where T : Component
    {
      var res = new List<T>();

      for (var i = 0; i < transform.childCount; i++)
        res.Add(transform.GetChild(i).GetComponent<T>());
      return res.ToArray();
    }
  }
}