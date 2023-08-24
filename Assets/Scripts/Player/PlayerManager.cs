using System.Collections;
using System.Collections.Generic;
using Game;
using Managers;
using Map;
using UnityEngine;

namespace Player
{
  public class PlayerManager : MonoBehaviourSingleton<PlayerManager>, IDontDestoryObject
  {
    public CanvasManager theCanvasManager;

    public Transform spawnPosition;

    public void Death()
    {
      theCanvasManager.Death();
      transform.position = GameManager.Map.currentRoom.startPosition.position;
    }
  }
}
