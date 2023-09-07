using UnityEngine;

namespace Managers
{
  public class ScriptableObjectCollection : ObjectCollection<ScriptableObject>
  {
#if UNITY_EDITOR
    private void Reset()
    {
      exts = "asset";
    }
    
    [ContextMenu("Apply Assets")]
    private void ApplyAsset() => FindObjectsAndAddToList();
#endif
  }
}
