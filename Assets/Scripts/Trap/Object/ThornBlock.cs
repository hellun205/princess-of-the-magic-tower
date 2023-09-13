using System;
using Interact;
using LinePath;
using UnityEngine;

namespace Trap.Object
{
  public class ThornBlock : Trap
  {
    [Header("Thorn Block")]
    [SerializeField]
    private LineFollower lineFollower;

    [SerializeField]
    private Interacter interacter;

    public float time;

    protected override void Activate()
    {
      base.Activate();
      lineFollower.time = time;
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