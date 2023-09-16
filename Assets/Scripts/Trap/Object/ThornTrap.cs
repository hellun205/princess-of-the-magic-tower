using Interact;
using UnityEngine;

namespace Trap.Object
{
  [RequireComponent(typeof(Collider2D), typeof(Interacter))]
  public class ThornTrap : Trap
  {
    private Animator anim;
    private Interacter interacter;

#if UNITY_EDITOR
    private void Reset()
    {
      var c = GetComponent<Collider2D>();
      var i = GetComponent<Interacter>();
      var layer = LayerMask.GetMask("Player");
      
      c.isTrigger = true;
      c.includeLayers = layer;
      c.excludeLayers = ~ layer;
      i.caster = InteractCaster.Others;
      i.currentCondition = InteractCondition.Reach;
      i.ignoreOpponentAttack = true;
    }
#endif
    
    protected override void Awake()
    {
      base.Awake();
      anim = GetComponent<Animator>();
      interacter = GetComponent<Interacter>();
    }

    protected override void Activate()
    {
      base.Activate();
      interacter.currentCondition = InteractCondition.Attack;
      anim.SetBool("activate" , true);
    }

    protected override void Deactivate()
    {
      base.Deactivate();
      interacter.currentCondition = InteractCondition.None;
      anim.SetBool("activate" , false);
    }
  }
}
