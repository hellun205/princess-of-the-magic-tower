using System;
using DG.Tweening;
using UnityEngine;
using Util;

namespace Map.Door
{
  public abstract class Door : MonoBehaviour
  {
    [SerializeField]
    protected Collider2D col;

    private Animator anim;

    [NonSerialized]
    public Room room;
    
    public abstract bool CheckClear { get; }
    
    public virtual void Close()
    {
      anim.SetBool("state", false);
    }

    public virtual void Open()
    {
      anim.SetBool("state", true);
    }

    protected void SetCollider(bool enable)
      => col.enabled = enable;

    protected void DisableCollider() => SetCollider(false);
    protected void EnableCollider() => SetCollider(true);

    public virtual void OnEntered()
    {
      Close();
      Utils.WaitUntil(() => CheckClear, () => Utils.Wait(1f, () =>
      {
        Open();
        room.isCleared = true;
      }));
    }

    private void Awake()
    {
      anim = GetComponent<Animator>();
    }
  }
}
