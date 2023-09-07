using DG.Tweening;
using UnityEngine;

namespace Map.Door
{
  public abstract class Door : MonoBehaviour
  {
    [SerializeField]
    protected BoxCollider2D col;

    public virtual void Close()
    {
      col.enabled = true;
    }

    public virtual void Open()
    {
      col.enabled = false;
    }
  }
}
