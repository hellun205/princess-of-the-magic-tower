using Interact;
using UnityEngine;

namespace Enemy.AI
{
  public abstract class EnemyAI : MonoBehaviour
  {
    protected EnemyController controller;

    protected virtual bool forceAttack => false;

    private bool _isAttacking;

    public bool isAttacking
    {
      get => _isAttacking;
      set
      {
        _isAttacking = value;
        if (forceAttack)
        {
          controller.interacter.currentCondition = InteractCondition.Attack;
          return;
        }
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