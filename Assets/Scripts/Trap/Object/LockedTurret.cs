using Managers;
using UnityEngine;
using Util;

namespace Trap.Object
{
  public class LockedTurret : Trap
  {
    [Header("Turret")]
    public float destroyTime;

    public DirectionSetter direction;

    public Transform firePos;

    [SerializeField]
    private Timer fireTimer = new Timer();

    private Animator animator;

    protected override void Awake()
    {
      base.Awake();
      animator = GetComponent<Animator>();
      fireTimer.onStart += OnTimerStart;
      fireTimer.onEnd += OnTimerEnd;
    }

    private void OnTimerStart(Timer sender)
    {
      Fire();
    }

    private void OnTimerEnd(Timer sender)
    {
      if(currentState)
        sender.Start();
    }

    private void Fire()
    {
      GameManager.Pool.Summon<Bullet>
      (
        "bullet/turret_bullet", firePos.position,
        obj => obj.SetBullet(direction.value, destroyTime)
      );

      animator.SetTrigger("isFire");
    }

    protected override void Activate()
    {
      base.Activate();
      fireTimer.Resume();
    }

    protected override void Deactivate()
    {
      base.Deactivate();
      fireTimer.Stop();
    }
  }
}
