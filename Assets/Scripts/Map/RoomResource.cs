using System;
using AYellowpaper.SerializedCollections;
using Map.Door;
using UnityEngine;
using Util;

namespace Map
{
  [CreateAssetMenu(fileName = "RoomResource", menuName = "Room/Resource", order = 0)]
  public class RoomResource : ScriptableObject
  {
    [Header("Camera setting")]
    public bool changeZoom = true;
    public float zoom = 10f;

    [Header("Door setting")]
    public DoorResource doorResource;

    [Header("Background layer setting")]
    public Sprite[] backgrounds;

    public Color[] bgColors;
    
  }
}
