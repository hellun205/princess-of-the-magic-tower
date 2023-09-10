using System;
using Managers;
using UnityEngine;

namespace Interact
{
  public abstract class InteractiveObject : MonoBehaviour
  {
    [Header("Interact Setting")]
    public InteractType type;

    public InteractCondition condition;

    protected abstract void OnInteract();

    private void OnTriggerStay2D(Collider2D collision)
    {
      if (!collision.CompareTag("Player")) return;
      CheckCondition(InteractType.Stay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (!collision.CompareTag("Player")) return;
      CheckCondition(InteractType.Enter);
    }

    private void CheckCondition(InteractType eventType)
    {
      if ((condition & InteractCondition.Dash) != 0 && GameManager.Player.move.isDashing)
        CheckType(eventType);
      if ((condition & InteractCondition.Walk) != 0)
        CheckType(eventType);
    }

    private void CheckType(InteractType eventType)
    {
      if (type != eventType) return;
      
      OnInteract();
    }
  }
}
