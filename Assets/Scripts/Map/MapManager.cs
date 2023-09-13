using System;
using Managers;
using Scene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Map
{
  public class MapManager : MonoBehaviour
  {
    public StageController controller { get; private set; }
    public Room currentRoom { get; private set; }

    private void Start()
    {
      OnSceneChanged();
    }

    public void OnSceneChanged()
    {
      controller = FindObjectOfType<StageController>();
      MoveTo(controller.startRoom);
    }

    public Room Find(string name) => controller.rooms.Find(x => x.name == name);

    public void MoveTo(string roomName, string beforeRoom = "", Link link = null)
    {
      var room = controller.rooms.Find(r => r.name == roomName);

      if (room is not null)
      {
        currentRoom = room;

        var playerPos = GameManager.Player.transform.position;
        Vector2 targetPos;

        if (!string.IsNullOrEmpty(beforeRoom) && link is not null)
        {
          var targetLinkPos = room.linkPositions.Find(t => t.name == beforeRoom);
          targetPos = new Vector2
          (
            link.ignoreX ? playerPos.x : targetLinkPos.position.x,
            link.ignoreY ? playerPos.y : targetLinkPos.position.y
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

    public void LoadStageFromSceneName(string sceneName, TransitionOption outT, TransitionOption inT)
    {
      new SceneLoader(sceneName)
       .Out(outT)
       .In(inT)
       .PauseOnTransitioning()
       .OnEndOut(() => GameManager.Pool.ClearPools())
       .OnStartIn(OnSceneChanged)
       .Load();
    }
  }
}
