using System.Collections;
using System.Collections.Generic;
using Game;
using Managers;
using Map;
using Scene;
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

    private void Update() {
      if(Input.GetKeyDown(KeyCode.F6)) {
        GameManager.Scene.Load("Test", new TransitionOption(Transitions.FADEOUT, 2), new TransitionOption(Transitions.FADEIN, 2), slowly:true);
      }
    }
  }
}
