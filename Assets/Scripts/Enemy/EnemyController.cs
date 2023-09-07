using System;
using Managers;
using Player.UI;
using Pool;
using UnityEngine;

namespace Enemy
{
  public class EnemyController : UsePool
  {
    public int maxHp;
    public int curHp;

    private EnemyMove enemyMove;
    
    public string map;

    public DashType killToAdd = DashType.Normal;
    
    protected override void Awake()
    {
      base.Awake();
      enemyMove = GetComponent<EnemyMove>();
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
      enemyMove.StartAI();
    }
    
    protected override void OnKill()
    {
      GameManager.Map.Find(map).enemies.Remove(pool.index);
      GameManager.Map.OnKillEnemy();
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
      GameManager.Player.Death();
    }
  }
}