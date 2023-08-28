using Managers;
using Pool;
using Pool.Extension;
using UnityEngine;

namespace Enemy
{
  public class NameTag : UsePool
  {
    [SerializeField]
    private Transform pos;

    private FollowingText ft;

    public string display;

    protected override void OnSummon()
    {
      ft = GameManager.Pool.Summon<FollowingText>("ui/followingtext", pos.position, o =>
      {
        o.SetText(display);
        o.follow.target = pos;
      });
    }

    protected override void OnKill()
    {
      ft.pool.Release();
    }
  }
}
