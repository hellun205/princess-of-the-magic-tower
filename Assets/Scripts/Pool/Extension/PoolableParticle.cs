using UnityEngine;
using Util;

namespace Pool.Extension
{
  public class PoolableParticle : UsePool
  {
    private new ParticleSystem particleSystem;

    public float duration { get; private set; }
    
    protected override void Awake()
    {
      base.Awake();
      particleSystem = GetComponent<ParticleSystem>();
      duration = particleSystem.main.duration;
    }

    protected override void OnSummon()
    {
      base.OnSummon();
      Utils.Wait(duration + 1f, () => pool.Release());
    }
  }
}
