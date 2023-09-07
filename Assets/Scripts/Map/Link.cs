using System;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Map
{
  [RequireComponent(typeof(CircleCollider2D))]
  public class Link: MonoBehaviour
  {
    [FormerlySerializedAs("collider")]
    public CircleCollider2D col;
    public string currentRoomName;
    
    private void Reset()
    {
      col = GetComponent<CircleCollider2D>();
      col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
      if (!col.CompareTag("Player")) return;

      GameManager.Map.MoveTo(name, currentRoomName);
    }
  }
}
