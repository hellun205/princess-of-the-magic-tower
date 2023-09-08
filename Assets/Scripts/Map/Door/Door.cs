using System;
using DG.Tweening;
using UnityEngine;
using Util;

namespace Map.Door
{
  public abstract class Door : MonoBehaviour
  {
    [SerializeField]
    protected SpriteRenderer sr;
    
    [SerializeField]
    protected BoxCollider2D col;

    [NonSerialized]
    public Room room;
    
    public abstract bool CheckClear { get; }
    
    public virtual void Close()
    {
      SetCollider(true);
      sr.DOFade(1f, 1f);
    }

    public virtual void Open()
    {
      sr.DOFade(0f, 1f).OnComplete(() => SetCollider(false));
    }

    protected void SetCollider(bool enable)
      => col.enabled = enable;

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
      Open();
    }
  }
}
