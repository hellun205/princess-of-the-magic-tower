using System;
using Managers;
using Map;
using UnityEngine;

namespace Player
{
  public class PlayerLocation : MonoBehaviour
  {
    public Room currentRoom;

    private bool follow = true;

    public void SetRoom(Room room)
    {
      currentRoom = room;
      transform.SetParent(currentRoom.transform);
    }

    private void Update()
    {
      if (!follow) return;
      transform.position = GameManager.Player.transform.position;
    }

    public Vector2 GetPositionInRoom()
      => transform.localPosition;

    public void SetPositionInRoom(Vector2 position)
    {
      follow = false;
      transform.localPosition = position;
      GameManager.Player.transform.position = transform.position;
      follow = true;
    }
  }
}