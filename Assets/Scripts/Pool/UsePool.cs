using System;
using UnityEngine;

namespace Pool
{
  [RequireComponent(typeof(PoolObject))]
  public abstract class UsePool : MonoBehaviour
  {
    [NonSerialized]
    public PoolObject pool;

    protected virtual void Awake()
    {
      pool = GetComponent<PoolObject>();
      pool.onGet += OnSummon;
      pool.onReleased += OnKill;
    }

    protected virtual void OnSummon()
    {
      
    }
    
    protected virtual void OnKill()
    {
      
    }
  }
}
