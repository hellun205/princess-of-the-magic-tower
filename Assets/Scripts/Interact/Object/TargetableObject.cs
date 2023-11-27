using System;
using Managers;
using Pool;
using Pool.Extension;
using UnityEngine;
using Util;

namespace Interact.Object
{
  public class TargetableObject : InteractiveObject
  {
    [Min(1)]
    public int maxHp;

    private int _curHp;

    public int curHp
    {
      get => _curHp;
      set
      {
        _curHp = value;
        if (hpBar != null)
          hpBar.hp = _curHp;
      }
    }

    public event Action onDead;

    private HpBar hpBar;
    private PoolObject po;
    
    private void Awake()
    {
      hpBar = GetComponent<HpBar>();
      po = GetComponent<PoolObject>();
      hpBar.maxHp = maxHp;
      hpBar.hp = curHp;
    }

    private void Start()
    {
      // hpBar.pool.onGet += () =>
      // {
      //   Utils.WaitUntil(() => hpBar.pb != null, () =>
      //   {
      //     hpBar.maxHp = maxHp;
      //     hpBar.hp = curHp;
      //   });
      // };
      
    }

    protected override void OnInteract(GameObject caster)
    {
      if (!caster.TryGetComponent<AttackableObject>(out var ao)) return;
      GameManager.Pool.Summon("effect/hit2", transform.position);
      curHp = Math.Max(0, curHp - ao.damage);
      if (curHp == 0) onDead?.Invoke();
    }
  }
}