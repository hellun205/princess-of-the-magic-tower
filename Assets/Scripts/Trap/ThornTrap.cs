using System;
using Managers;
using UnityEngine;

namespace Trap
{
  [RequireComponent(typeof(Collider2D))]
  public class ThornTrap : Trap
  {
    private Animator anim;
    
    // [Header("Thorn Trap")]
    private Collider2D col;

#if UNITY_EDITOR
    private void Reset()
    {
      var c = GetComponent<Collider2D>();
      var layer = LayerMask.GetMask("Player");
      
      c.isTrigger = true;
      c.includeLayers = layer;
      c.excludeLayers = ~ layer;
    }
#endif
    
    protected override void Awake()
    {
      base.Awake();
      anim = GetComponent<Animator>();
      col = GetComponent<Collider2D>();
    }

    protected override void Activate()
    {
      base.Activate();
      col.enabled = true;
      anim.SetBool("activate" , true);
    }

    protected override void Deactivate()
    {
      base.Deactivate();
      col.enabled = false;
      anim.SetBool("activate" , false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
      GameManager.Player.Death();
    }
  }
}
