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

    /// <summary>
    /// 풀 객체가 생성되었을 때 실행됩니다.
    /// </summary>
    protected virtual void OnSummon()
    {
      
    }
    
    /// <summary>
    /// 풀 객체가 삭제되었을 때 실행됩니다.
    /// </summary>
    protected virtual void OnKill()
    {
      
    }
  }
}
