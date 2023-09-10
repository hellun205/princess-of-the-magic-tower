using System;
using Interact;
using UnityEngine;

namespace Enemy.AI
{
  public abstract class EnemyAI : MonoBehaviour
  {
    protected EnemyController controller;

    private bool _isAttacking;

    public bool isAttacking
    {
      get => _isAttacking;
      set
      {
        _isAttacking = value;
        controller.interacter.currentCondition = value ? InteractCondition.Attack : InteractCondition.Reach;
        if (!value)
          controller.interacter.RemoveDetection();
      }
    }

    public abstract void StartAI();

    protected virtual void Awake()
    {
      controller = GetComponent<EnemyController>();
    }
  }
}