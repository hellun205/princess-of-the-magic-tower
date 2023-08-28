using System;
using Managers;
using UnityEngine;

namespace Pool
{
  public class PoolSummoner : MonoBehaviour
  {
    public string target;
    public bool summonOnStart;

    private void Start()
    {
      if (!summonOnStart) return;
      Summon();
    }

    private void Summon()
    {
      GameManager.Pool.Summon(target, transform.position);
    }
  }
}
