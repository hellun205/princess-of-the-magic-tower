using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using Util;

namespace Map.Door
{
  [CreateAssetMenu(fileName = "DoorResource", menuName = "Room/DoorResource", order = 0)]
  public class DoorResource : ScriptableObject
  {
    [SerializedDictionary("Direction", "Sprites")]
    public SerializedDictionary<Direction, Sprite[]> animateSprite;
    
    private void Reset()
    {
      animateSprite = new SerializedDictionary<Direction, Sprite[]>
      {
        { Direction.Top, Array.Empty<Sprite>() },
        { Direction.Right, Array.Empty<Sprite>() },
        { Direction.Bottom, Array.Empty<Sprite>() },
        { Direction.Left, Array.Empty<Sprite>() },
      };
    }
  }
}