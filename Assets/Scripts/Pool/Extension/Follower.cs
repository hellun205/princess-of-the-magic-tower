using System;
using UnityEngine;

namespace Pool.Extension
{
  public class Follower : UsePool
  {
    [NonSerialized]
    public Transform target;

    private void Update()
    {
      if (target is null) return;

      pool.position = target.position;
    }
  }
}
