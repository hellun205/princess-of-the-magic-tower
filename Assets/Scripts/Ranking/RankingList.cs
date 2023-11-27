using System;
using UnityEngine;
using Util;
using Object = UnityEngine.Object;

namespace Ranking
{
  public abstract class RankingList<T> : MonoBehaviour where T : Object
  {
    public Transform contentBox;

    public T prefabItem;

    protected virtual void Awake()
    {
      Initalize();
    }

    protected abstract void Initalize();

    protected void Clear()
    {
      var childs = contentBox.GetChilds();
      foreach (var child in childs)
        Destroy(child); 
    }

    protected T CreateObject()
    {
      return Instantiate(prefabItem, contentBox);
    }
  }
}
