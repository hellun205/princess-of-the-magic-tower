using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Map
{
  public class MapController : MonoBehaviour
  {
    public string mainRoom;
    
    public List<Room> rooms;

    private void Awake()
    {
      rooms = GetComponentsInChildren<Room>().ToList();
    }
  }
}
