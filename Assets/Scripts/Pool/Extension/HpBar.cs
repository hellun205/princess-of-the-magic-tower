using System;
using Managers;
using UnityEngine;

namespace Pool.Extension
{
  public class HpBar : UsePool
  {
    public DividedProgressBar pb { get; private set; }

    [SerializeField]
    private Transform pos;

    private int _maxHp;
    private int _hp;

    public int maxHp
    {
      get => _maxHp;
      set
      {
        _maxHp = value;
        if (pb != null)
          pb.maxValue = _maxHp;
      }
    }

    public int hp
    {
      get => _hp;
      set
      {
        _hp = value;
        if (pb != null)
          pb.value = _hp;
      }
    }

    protected override void OnSummon()
    {
      pb = GameManager.Pool.Summon<DividedProgressBar>("ui/hpbar", SetHpBarPosition());
      pb.value = hp;
      pb.maxValue = maxHp;
    }

    private void Update()
    {
      if (pb != null)
        SetHpBarPosition();
    }

    protected override void OnKill()
    {
      if (pb != null)
        pb.pool.Release();
    }

    private Vector3 SetHpBarPosition()
    {
      var pos = this.pos == null ? transform.position + new Vector3(0f, 1f) : this.pos.position;
      if (pb != null)
        pb.pool.position = pos;

      return pos;
    }
  }
}