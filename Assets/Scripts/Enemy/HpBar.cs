using System;
using Managers;
using Pool;
using Pool.Extension;
using UnityEngine;

namespace Enemy
{
  [RequireComponent(typeof(EnemyController))]
  [Obsolete]
  public class HpBar : UsePool
  {
    public Transform pos;
    public bool isText;
    private ProgressBar progressBar;
    private EnemyController enemyController;
    private bool isEnabled;

    protected override void Awake()
    {
      enemyController = GetComponent<EnemyController>();
      base.Awake();
    }

    protected override void OnSummon()
    {
      progressBar = GameManager.Pool.Summon<ProgressBar>("ui/progressbar", pos.position);
      isEnabled = true;
    }

    protected override void OnKill()
    {
      isEnabled = false;
      progressBar.pool.Release();
    }

    private void Update()
    {
      if (!isEnabled) return;

      // progressBar.progressBar.isText = isText;
      // progressBar.progressBar.maxValue = enemyController.maxHp;
      // progressBar.progressBar.value = enemyController.curHp;
    }
  }
}
