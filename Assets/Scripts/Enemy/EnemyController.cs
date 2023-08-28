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

    public void Hit()
    {
      
    }
  }
}
