using System;
using AYellowpaper.SerializedCollections;
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

    [Header("Door setting"), SerializedDictionary("Direction", "Sprites")]
    public SerializedDictionary<Direction, Sprite[]> doorSprites;

    [Header("Background layer setting")]
    public Sprite[] backgrounds;

    public Color[] bgColors;

#if UNITY_EDITOR
    private void Reset()
    {
      doorSprites = new()
      {
        { Direction.Top, Array.Empty<Sprite>() },
        { Direction.Right, Array.Empty<Sprite>() },
        { Direction.Bottom, Array.Empty<Sprite>() },
        { Direction.Left, Array.Empty<Sprite>() },
      };
    }
#endif
  }
}
