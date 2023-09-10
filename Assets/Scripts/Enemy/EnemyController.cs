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
    public int maxHp;
    public int curHp;
    
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
    }
    
    public void SetMap(string value)
    {
      map = value;
      GameManager.Map.Find(map).AddEnemy(pool.index);
    }

    protected override void OnSummon()
    {
      curHp = maxHp;
    }

    public void FindPlayer()
    {
      move.StartAI();
    }
    
    protected override void OnKill()
    {
      GameManager.Map.Find(map).enemies.Remove(pool.index);
    }

    public void Hit(int damage)
    {
      curHp = Math.Max(0, curHp - damage);

      if (curHp == 0) Dead();
    }

    private void Dead()
    {
      if ((killToAdd & DashType.Normal) != 0)
        GameManager.Player.skill.ReloadDash();
      if ((killToAdd & DashType.Additional) != 0)
        GameManager.Player.skill.AddAdditionalDash();
      
      pool.Release();
    }

    public static void AttackPlayer()
    {
      if (GameManager.Player.testMode) return;

      GameManager.Player.Death();
    }
  }
}