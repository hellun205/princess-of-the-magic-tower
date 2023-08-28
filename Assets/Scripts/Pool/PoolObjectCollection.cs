using Managers;
using UnityEngine;

namespace Pool
{
  public class PoolObjectCollection : ObjectCollection<PoolObject>
  {
#if UNITY_EDITOR
    private void Reset()
    {
      root = "PoolObjects";
      exts = "prefab";
    }

    [ContextMenu("Apply Assets")]
    public void ApplyAssets()
    {
      FindObjectsAndAddToList();
    }
#endif
  }
}
