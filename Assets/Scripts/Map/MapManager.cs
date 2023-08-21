using System;
using Managers;

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
        GameManager.Player.transform.position = room.startPosition.position;
      }
      else

        throw new Exception($"invalid room name: {roomName}");
    }
  }
}
