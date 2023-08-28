using System;
using Managers;
using Pool;
using UnityEngine;

namespace Enemy
{
  public class EnemyController : UsePool
  {
    public int maxHp;
    public int curHp;

    protected override void OnSummon()
    {
      curHp = maxHp;
    }

    public void Hit(int damage)
    {
      curHp = Math.Max(0, curHp - damage);
      
      if (curHp == 0) Dead();
    }

    private void Dead()
    {
      GameManager.Player.skill.ReloadDash();
      pool.Release();
    }
  }
}
