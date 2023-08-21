using System;
using Managers;
using UnityEngine;

namespace Map
{
  public class MapManager : MonoBehaivorSingleton<MapManager>
  {
    public MapController Controller { get; private set; }
    public Room currentRoom { get; private set; }

    private void Start()
    {
      OnSceneChanged();
    }

    public void OnSceneChanged()
    {
      Controller = FindObjectOfType<MapController>();
      MoveTo(Controller.mainRoom);
    }

    public void MoveTo(string roomName, string link = "")
    {
      var room = Controller.rooms.Find(r => r.name == roomName);

      if (room is not null)
      {
        currentRoom = room;
        // 
        GameManager.Player.transform.position = string.IsNullOrEmpty(link) switch
        {
          true  => room.startPosition.position,
          false => room.linkPositions.Find(t => t.name == link).position
        };

      }
      else

        throw new Exception($"invalid room name: {roomName}");
    }
  }
}
