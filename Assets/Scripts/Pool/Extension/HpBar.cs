using Managers;
using UnityEngine;

namespace Pool.Extension
{
  public class HpBar : UsePool
  {
    public DividedProgressBar pb { get; private set; }
    
    protected override void OnSummon()
    {
      base.OnSummon();
      pb = GameManager.Pool.Summon<DividedProgressBar>("ui/divied")
    }
  }
}
