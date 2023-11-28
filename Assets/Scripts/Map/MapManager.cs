using System;
using System.Linq;
using Interact.Object;
using Managers;
using Player;
using Scene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Map
{
  public class MapManager : MonoBehaviour
  {
    public StageController controller { get; private set; }
    public Room currentRoom { get; private set; }

    public bool moveOnStart { get; set; } = true;

    private void Start()
    {
      OnSceneChanged();
    }

    public void OnSceneChanged()
    {
      ReloadStage();

      if (GameManager.HasSave())
      {
        var data = GameManager.LoadData();

        if (data.stage == SceneManager.GetActiveScene().name)
        {
          FindObjectsOfType<SavePoint>()
            .SingleOrDefault(x => x.gameObject.name == data.objectName)
            ?.anim.Play("ForceOpen");
        }
      }

      if (moveOnStart)
        MoveTo(controller.startRoom);

      moveOnStart = true;

      GameManager.Manager.stopwatchObject.isRunning = true;
    }

    public void ReloadStage()
    {
      controller = FindObjectOfType<StageController>();
    }

    public Room Find(string name) => controller.rooms.Find(x => x.name == name);

    public void MoveTo(string roomName, Door.Door door = null)
    {
      var specific = string.Empty;
      if (roomName.Contains('.'))
      {
        var split = roomName.Split('.');
        specific = split[1];
        roomName = split[0];
      }

      if (FindObjectOfType<PlayerLocation>() == null)
        GameManager.PlayerLocation =
          new GameObject("@player_location", typeof(PlayerLocation)).GetComponent<PlayerLocation>();

      var room = controller.rooms.Find(r => r.name == roomName);

      if (room is not null)
      {
        if (currentRoom != null) currentRoom.OnExited();
        currentRoom = room;
        GameManager.PlayerLocation.SetRoom(currentRoom);

        var playerPos = GameManager.Player.transform.position;
        Vector2 targetPos;

        if (door is not null)
        {
          var targetLinkPos = room.linkPositions.Find(t =>
            t.name == (string.IsNullOrEmpty(specific) ? door.room.name : $"{door.room.name}.{specific}"));
          targetPos = new Vector2
          (
            door.ignoreX ? playerPos.x : targetLinkPos.position.x,
            door.ignoreY ? playerPos.y : targetLinkPos.position.y
          );
        }
        else
          targetPos = room.startPosition.position;

        GameManager.Player.transform.position = targetPos;

        room.OnEntered();
      }
      else
        throw new Exception($"invalid room name: {roomName}");
    }

    public void LoadCurrentStage()
      => LoadStageFromSceneName(SceneManager.GetActiveScene().name);

    public void LoadCurrentStage(TransitionOption outT, TransitionOption inT)
      => LoadStageFromSceneName(SceneManager.GetActiveScene().name, outT, inT);

    public void LoadStageFromSceneName(string sceneName)
      => LoadStageFromSceneName(sceneName, new(Transitions.FADEOUT, 2f), new(Transitions.FADEIN, 2f));

    public void LoadStageFromSceneName
    (
      string sceneName,
      TransitionOption outT,
      TransitionOption inT,
      Action onEndOut = null
    )
    {
      new SceneLoader(sceneName)
        .Out(outT)
        .In(inT)
        .PauseOnTransitioning()
        .OnEndOut(() =>
        {
          GameManager.Pool.ClearPools();
          onEndOut?.Invoke();
        })
        .OnStartIn(OnSceneChanged)
        .Load();
    }
  }
}