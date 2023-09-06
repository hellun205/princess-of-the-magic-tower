using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Util
{
  public class Binder
  {
    public Dictionary<string, GameObject> objects = new();

    private Transform tr;

    public Binder(MonoBehaviour mb)
    {
      tr = mb.transform;
    }

    public Binder(GameObject go)
    {
      tr = go.transform;
    }

    /// <summary>
    /// 특수문자를 포함 한 자식 객체들을 찾고 바인딩 합니다.
    /// </summary>
    /// <param name="findChr">특수문자</param>
    /// <returns>Binder</returns>
    public Binder Bind(char findChr)
    {
      var find = tr.GetComponentsInChildren<Transform>().Where(t => t.name.Contains(findChr));

      foreach (var obj in find)
      {
        var name = obj.name.Replace(findChr.ToString(), "");

        if (!objects.ContainsKey(name))
          objects.Add(name, obj.gameObject);
      }

      return this;
    }

    /// <summary>
    /// 바인딩된 객체들을 모두 삭제합니다.
    /// </summary>
    /// <returns>Binder</returns>
    public Binder Clear()
    {
      objects.Clear();

      return this;
    }

    /// <summary>
    /// 바인딩된 객체 중에서 특수문자가 포함 된 객체들을 삭제합니다.
    /// </summary>
    /// <param name="findChr">특수문자</param>
    /// <returns>Binder</returns>
    public Binder Remove(char findChr)
    {
      var find = objects.Values
       .Where(o => o.name.Contains(findChr))
       .Select(o => o.name.Replace(findChr.ToString(), ""));

      foreach (var name in find)
      {
        if (objects.ContainsKey(name))
          objects.Remove(name);
        else
        {
          Debug.LogError($"doesn't exist binded object with symbol: \"{findChr}\"");
          break;
        }
      }

      return this;
    }

    /// <summary>
    /// 바인딩된 객체를 삭제합니다.
    /// </summary>
    /// <param name="names">객체 이름</param>
    /// <returns>Binder</returns>
    public Binder Remove(params string[] names)
    {
      foreach (var name in names)
      {
        if (objects.ContainsKey(name))
          objects.Remove(name);
        else
          Debug.LogError($"doesn't exist binded object: \"{name}\"");
      }

      return this;
    }

    /// <summary>
    /// 바인딩 객체를 가져옵니다.
    /// </summary>
    /// <param name="name">객체 이름</param>
    public GameObject this[string name] => Get(name);

    /// <summary>
    /// 바인딩 객체를 가져옵니다.
    /// </summary>
    /// <param name="name">객체 이름</param>
    /// <returns>객체</returns>
    public GameObject Get(string name) => objects[name];

    /// <summary>
    /// 바인딩 객체의 컴포넌트를 가져옵니다.
    /// </summary>
    /// <param name="name">객체 이름</param>
    /// <typeparam name="T">컴포넌트</typeparam>
    /// <returns>객체 컴포넌트</returns>
    public T Get<T>(string name) where T : Component
      => Get(name).GetComponent<T>();
  }
}
