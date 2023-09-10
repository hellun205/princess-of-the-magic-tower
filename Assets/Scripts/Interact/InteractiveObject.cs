using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Interact
{
  public abstract class InteractiveObject : MonoBehaviour
  {
    [Header("Interact Setting")]
    public InteractType type;

    public InteractCaster detectCaster;

    public InteractCondition condition;

    private List<GameObject> interacters = new();

    protected abstract void OnInteract(GameObject caster);

    public void Interact(InteractCondition condition, GameObject go)
    {
      if ((this.condition & condition) == 0) return;

      if (!interacters.Contains(go))
        interacters.Add(go);
      else if (type == InteractType.First) return;

      OnInteract(go);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      Remove(other.gameObject);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
      Remove(other.gameObject);
    }

    public void Remove(GameObject go)
    {
      if (interacters.Contains(go))
        interacters.Remove(go);
    }
  }
}
