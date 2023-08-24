using System;
using Managers;
using UnityEngine;

namespace Map
{
  [RequireComponent(typeof(CircleCollider2D))]
  public class Link: MonoBehaviour
  {
    public CircleCollider2D collider;
    public string currentRoomName;
    
    private void Reset()
    {
      collider = GetComponent<CircleCollider2D>();
      collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
      if (!col.CompareTag("Player")) return;

      GameManager.Map.MoveTo(name, currentRoomName);
    }
  }
}
