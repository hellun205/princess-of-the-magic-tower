using System;
using System.Collections;
using System.Linq;
using Managers;
using Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

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

    public void MoveTo(string roomName, string link = "")
    {
      var room = controller.rooms.Find(r => r.name == roomName);

      if (room is not null)
      {
        currentRoom = room;

        GameManager.Player.transform.position = string.IsNullOrEmpty(link) switch
        {
          true => room.startPosition.position,
          false => room.linkPositions.Find(t => t.name == link).position
        };

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