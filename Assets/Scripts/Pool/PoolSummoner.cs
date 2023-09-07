using System;
using Enemy;
using Managers;
using UnityEngine;

namespace Pool
{
  public class PoolSummoner : MonoBehaviour
  {
    public string target;
    public bool summonOnStart;
    
    [NonSerialized]
    public string room;

    private void Start()
    {
      if (!summonOnStart) return;
      Summon();
    }

    private void Summon()
    {
      GameManager.Pool.Summon<EnemyController>(target, transform.position, obj => obj.SetMap(room));
    }
  }
}
