using UnityEngine;

namespace Util
{
  public abstract class UIMonoBehaviour : MonoBehaviour
  {
    public RectTransform rectTransform { get; private set; }
    
    protected virtual void Awake()
    {
      rectTransform = GetComponent<RectTransform>();
    }
  }
}
