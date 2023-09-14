using System;
using UnityEngine;
using Util;

namespace Map.Door
{
  public abstract class Door : MonoBehaviour, IRequireRoom, IRoomEnteredEventHandler
  {
    public Direction direction;

    protected Collider2D col;

    private Animator anim;
    
    public Room room { protected get; set; }

    [NonSerialized]
    public SpriteRenderer spriteRenderer;

    public abstract bool CheckClear { get; }

    [ContextMenu("Close")]
    public virtual void Close()
    {
      anim.SetBool("force", false);
      anim.SetBool("state", false);
    }

    [ContextMenu("Open")]
    public virtual void Open()
    {
      anim.SetBool("force", false);
      anim.SetBool("state", true);
    }

    [ContextMenu("ForceOpen")]
    public virtual void ForceOpen()
    {
      anim.SetBool("force", true);
      anim.SetBool("state", true);
    }

    [ContextMenu("ForceClose")]
    public virtual void ForceClose()
    {
      anim.SetBool("force", true);
      anim.SetBool("state", false);
    }

    protected void SetCollider(bool enable)
      => col.enabled = enable;

    protected void DisableCollider() => SetCollider(false);

    protected void EnableCollider() => SetCollider(true);


    public virtual void OnEntered()
    {
      if (CheckClear)
      {
        ForceOpen();
        return;
      }

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
      col = GetComponent<Collider2D>();
      spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(int index)
      => spriteRenderer.sprite = room.doorSprites[direction][index];

    public void OnRoomEntered() => OnEntered();
  }
}
