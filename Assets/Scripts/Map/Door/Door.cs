using System;
using Interact;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Map.Door
{
  public class Door : InteractiveObject, IRequireRoom, IRoomEnterEventHandler
  {
    [Header("Door option")]
    public Direction direction;

    protected Collider2D col;

    private Animator anim;
    
    public Room room { get; set; }

    [FormerlySerializedAs("type")]
    public DoorType doorType;

    public bool state;

    public int clearCount = 0;

    [Header("Link option")]
    public bool ignoreX;
    
    public bool ignoreY;

    [NonSerialized]
    public SpriteRenderer spriteRenderer;

    public bool CheckClear => this.GetCondition();

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
      => col.isTrigger = !enable;


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

      type = InteractType.First;
      condition = InteractCondition.Reach | InteractCondition.Attack;
      detectCaster = InteractCaster.Player;
    }

    public void SetSprite(int index)
      => spriteRenderer.sprite = room.resourceData.doorResource.animateSprite[direction][index];
    
    public void OnRoomEntered() => OnEntered();

#if UNITY_EDITOR
    private void Reset()
    {
      col = GetComponent<Collider2D>();
      col.isTrigger = true;
    }
#endif
    
    protected override void OnInteract(GameObject caster)
    {
      GameManager.Map.MoveTo(name, this);
    }

    public override bool AdditionalCondition() => col.isTrigger;
  }
}
