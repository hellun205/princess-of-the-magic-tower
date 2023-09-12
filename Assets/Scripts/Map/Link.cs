using System;
using Managers;
using UnityEngine;

namespace Map
{
  [RequireComponent(typeof(Collider2D))]
  public class Link : MonoBehaviour
  {
    private Collider2D col;

    public bool ignoreX;
    
    public bool ignoreY;
    
    [NonSerialized]
    public string currentRoomName;

#if UNITY_EDITOR
    private void Reset()
    {
      col = GetComponent<Collider2D>();
      col.isTrigger = true;
    }
#endif
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (!other.CompareTag("Player")) return;

      GameManager.Map.MoveTo(name, currentRoomName, this);
    }
  }
}
