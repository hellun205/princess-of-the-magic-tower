using System;
using Managers;
using UnityEngine;

namespace Map
{
  public class MapManager : MonoBehaviourSingleton<MapManager>
  {
    public StageController controller { get; private set; }
    public Room currentRoom { get; private set; }

    private void Start() {
      OnSceneChanged();
    }

    public void OnSceneChanged() {
      controller = FindObjectOfType<StageController>();
      MoveTo(controller.mainRoom);
    }

    public Room Find(string name) => controller.rooms.Find(x => x.name == name);

    public void MoveTo(string roomName, string link = "") {
      var room = controller.rooms.Find(r => r.name == roomName);

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

