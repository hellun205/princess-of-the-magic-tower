using System;
using Enemy;
using Managers;
using UnityEngine;

namespace Pool
{
  public class PoolSummoner : MonoBehaviour
  {
    public string target;
    
    [NonSerialized]
    public string room;

    public void Summon()
    {
      GameManager.Pool.Summon<EnemyController>(target, transform.position, obj => obj.SetMap(room));
    }
  }
}
