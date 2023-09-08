using System;
using Managers;
using Player.UI;
using UnityEngine;

namespace Player
{
  public class PlayerSkill : MonoBehaviour
  {
    private DashCountBar dashBar;
    private PlayerMove thePlayerMove;

    [NonSerialized]
    public int dashCount;
    
    [NonSerialized]
    public int additionalDashCount;

    public int maxDashCount = 3;

    private float currentDashCooltime;
    public float maxDashCooltime;

    private void Awake()
    {
      thePlayerMove = GetComponent<PlayerMove>();
    }

    private void Start()
    {
      dashBar = GameManager.ManagedObject.Get<DashCountBar>("dashbar");
      currentDashCooltime = maxDashCooltime;
      ReloadDash(maxDashCount);
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        Dash();
      }

      if (dashCount >= maxDashCount)
      {
        currentDashCooltime = maxDashCooltime;
        return;
      }

      currentDashCooltime -= Time.deltaTime;

      if (currentDashCooltime > 0) return;

      currentDashCooltime = maxDashCooltime;
      ReloadDash();
    }

    public void Dash()
    {
      if (dashCount + additionalDashCount <= 0) return;

      UseDash();
      thePlayerMove.Dash();
    }

    public void ReloadDash(int amount = 1)
    {
      dashCount = Math.Min(maxDashCount, dashCount + amount);
      dashBar.AddCount(amount);
    }

    public void AddAdditionalDash(int amount = 1)
    {
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
    }
  }
}
