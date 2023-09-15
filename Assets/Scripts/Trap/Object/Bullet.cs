using System.Collections;
using Pool;
using UnityEngine;
using Util;
using Vector2 = UnityEngine.Vector2;

namespace Trap.Object
{
  public class Bullet : UsePool
  {
    public float speed;
  
    private float lifeTime;
    
    private Vector2 direction;

    private Coroutiner fireCrt;

    protected override void Awake()
    {
      base.Awake();
      fireCrt = new Coroutiner(FireRoutine);
    }

    public void SetBullet(Vector2 direction, float lifeTime)
    {
      this.direction = direction;
      this.lifeTime = lifeTime;
    }

    private IEnumerator FireRoutine()
    {
      Utils.Wait(lifeTime, pool.Release);

      while (true)
      {
        transform.Translate((direction * speed) * Time.deltaTime);
        yield return new WaitForEndOfFrame();
      }
    }

    protected override void OnSummon()
    {
      fireCrt.Start();
    }

    protected override void OnKill()
    {
      fireCrt.Stop();
    }
  }
}