using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
  public class StageController : MonoBehaviour
  {
    public string startRoom;

    [NonSerialized]
    public List<Room> rooms;

    private void Awake()
    {
      rooms = GetComponentsInChildren<Room>().ToList();
    }
  }
}
