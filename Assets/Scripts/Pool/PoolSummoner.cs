using System;
using Enemy;
using Managers;
using Map;
using UnityEngine;

namespace Pool
{
  public class PoolSummoner : MonoBehaviour, IRequireRoom, IRoomEnterEventHandler
  {
    public string target;

    public Room room { protected get; set; }

    public void Summon()
    {
      GameManager.Pool.Summon<EnemyController>(target, transform.position, obj => obj.SetMap(room.name));
    }

    public void OnRoomEntered()
    {
      if (room.isCleared) return;

      Summon();
    }
  }
}