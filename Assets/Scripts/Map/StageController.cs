using System;
using System.Collections.Generic;
using System.Linq;
using Scene;
using UnityEngine;
using UnityEngine.Serialization;

namespace Map
{
  public class StageController : MonoBehaviour
  {
    public int stageIndex;

    public string mainRoom;
    
    public List<Room> rooms;

    private void Awake()
    {
      rooms = GetComponentsInChildren<Room>().ToList();
    }

    public void LoadStage(int index)
    {
      
    }

    public void LoadStageFromSceneName(string sceneName)
    {
      
    }
  }
}
