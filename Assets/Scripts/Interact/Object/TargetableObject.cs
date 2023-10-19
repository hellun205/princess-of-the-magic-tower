using System;
using Managers;
using UnityEngine;

namespace Interact.Object
{
  public class TargetableObject : InteractiveObject
  {
    [Min(1)]
    public int maxHp;
    
    [NonSerialized]
    public int curHp;

    public event Action onDead;
    
    protected override void OnInteract(GameObject caster)
    {
      if (!caster.TryGetComponent<AttackableObject>(out var ao)) return;
      GameManager.Pool.Summon("effect/hit", transform.position);
      curHp = Math.Max(0, curHp - ao.damage);
      if (curHp == 0) onDead?.Invoke();
    }
  }
}
