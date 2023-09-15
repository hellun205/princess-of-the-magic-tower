using Interact;
using LinePath;
using UnityEngine;
using Util;

namespace Trap.Object
{
  public class ThornBlock : Trap
  {
    [Header("Thorn Block")]
    [SerializeField]
    private LineFollower lineFollower;

    [SerializeField]
    private Interacter interacter;

    public float duration;

    public TimerType timerType = TimerType.Normal;

    protected override void Activate()
    {
      base.Activate();
      lineFollower.duration = duration;
      lineFollower.type = timerType;
      lineFollower.isEnabled = true;
      interacter.currentCondition = InteractCondition.Attack;
    }

    protected override void Deactivate()
    {
      base.Deactivate();
      lineFollower.isEnabled = false;
      interacter.currentCondition = InteractCondition.None;
    }
  }
}