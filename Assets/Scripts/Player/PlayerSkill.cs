using System;
using Managers;
using Player.UI;
using UI;
using UnityEngine;
using Util;

namespace Player
{
  public class PlayerSkill : MonoBehaviour
  {
    private DashCountBar dashBar;
    private CooledTouch dashBtn;
    private PlayerMove thePlayerMove;

    [NonSerialized]
    public int dashCount;
    
    [NonSerialized]
    public int additionalDashCount;

    public int maxDashCount = 3;

    public float cooldownTime;
    private Timer cooldownTimer;

    private void Awake()
    {
      thePlayerMove = GetComponent<PlayerMove>();
      cooldownTimer = new Timer(cooldownTime);
      cooldownTimer.onBeforeStart += t => t.duration = cooldownTime;
      cooldownTimer.onEnd += t =>
      {
          ReloadDash();
        Utils.WaitUntil(() => dashCount < maxDashCount, () => t.Start());
      };
    }

    private void Start()
    {
      dashBar = GameManager.ManagedObject.Get<DashCountBar>("dashbar");
      dashBtn = GameManager.ManagedObject.Get<CooledTouch>("button_dash");
      dashBtn.onTouch += _ => Dash();

      cooldownTimer.Start();
      ResetDashCount();
    }

    public void Dash()
    {
      if (dashCount + additionalDashCount <= 0) return;

      UseDash();
      thePlayerMove.Dash();
    }

    public void ReloadDash(int amount = 1)
    {
      if (dashCount >= maxDashCount) return;
      dashCount +=  amount;
      dashBar.AddCount(amount);
      if (dashCount > 0)
        Utils.WaitUntil(() => !dashBtn.isCooldown, () => dashBtn.isEnabled = true);
    }

    public void AddAdditionalDash(int amount = 1)
    {
      if (additionalDashCount >= 1) return;
      additionalDashCount += amount;
      dashBar.AddCount(amount, DashType.Additional);
    }

    public void UseDash(int amount = 1)
    {
      if (additionalDashCount > 0)
        additionalDashCount = Math.Max(0, additionalDashCount - amount);
      else
        dashCount = Math.Max(0, dashCount - amount);
      
      dashBar.SubCount(amount);

      if (dashCount == 0)
        dashBtn.isEnabled = false;
    }

    public void ResetDashCount()
    {
      dashBar.ClearCount();
      dashCount = 0;
      additionalDashCount = 0;
      ReloadDash(maxDashCount);
    }
  }
}
