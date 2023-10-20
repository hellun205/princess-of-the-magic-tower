using System;
using Interact;
using Interact.Object;
using Managers;
using Player.UI;
using Pool;
using UnityEngine;

namespace Enemy
{
  public class EnemyController : UsePool, IObstacleDestroyable
  {
    [field: SerializeField]
    [field: Min(0)]
    public int destroyLevel { get; set; }

    [NonSerialized]
    public EnemyMove move;

    [NonSerialized]
    public Interacter interacter;
    
    public string map;

    public DashType killToAdd = DashType.Normal;
    
    protected override void Awake()
    {
      base.Awake();
      move = GetComponent<EnemyMove>();
      interacter = GetComponent<Interacter>();
      if (TryGetComponent<TargetableObject>(out var to))
        to.onDead += Dead;
    }
    
    public void SetMap(string value)
    {
      Debug.Log(value);
      map = value;
      GameManager.Map.Find(map).AddEnemy(pool.index);
    }

    protected override void OnSummon()
    {
      if (TryGetComponent<TargetableObject>(out var to))
        to.curHp = to.maxHp;

      move.StartAI();
    }
    
    protected override void OnKill()
    {
      GameManager.Map.Find(map).enemies.Remove(pool.index);
    }

    private void Dead()
    {
      if ((killToAdd & DashType.Normal) != 0)
        GameManager.Player.skill.ReloadDash();
      if ((killToAdd & DashType.Additional) != 0)
        GameManager.Player.skill.AddAdditionalDash();
      
      pool.Release();
    }
  }
}