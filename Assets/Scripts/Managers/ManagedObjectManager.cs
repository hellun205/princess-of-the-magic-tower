using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
  public class ManagedObjectManager : MonoBehaviour
  {
    public List<GameObject> items;

    private void Awake()
    {
      items = FindObjectsOfType<GameObject>().Where(tmp => tmp.name.Contains('$')).ToList();
    }

    public GameObject Find(string name, Action<GameObject> setter = null)
    {
      var obj = items.Find(tmp => tmp.name == name);

      if (obj == null)
        throw new Exception($"invalid managed object: \"{name}\"");
      
      setter?.Invoke(obj);
      return obj;
    }

    public T Find<T>(string name, Action<T> setter = null) where T : UnityEngine.Object
    {
      var obj = items.Find(tmp => tmp.name == name).GetComponent<T>();

      if (obj == null)
        throw new Exception($"invalid managed object: \"{name}\" (\"{typeof(T).Name}\")");
      
      setter?.Invoke(obj);
      return obj;
    }
  }
}