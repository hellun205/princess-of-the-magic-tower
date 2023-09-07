using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managers;
using Random = UnityEngine.Random;

namespace Util
{
  public static class Utils
  {
    public static bool Approximately(this float a, float b, float tolerance = 0.1f)
    {
      return Mathf.Abs(a - b) < tolerance;
    }
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
    public static void Wait(float second, Action fn) => GameManager.Manager.StartCoroutine(WaitRoutine(second, fn));

    private static IEnumerator WaitRoutine(float second, Action fn)
    {
      yield return new WaitForSecondsRealtime(second);
      fn.Invoke();
    }

    public static Vector3 WorldToScreenSpace(this RectTransform canvas, Vector3 worldPos)
    {
      var screenPoint = Camera.main.WorldToScreenPoint(worldPos);
      screenPoint.z = 0;

      if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, screenPoint, Camera.main, out var screenPos))
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
  }
}
