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
    // private Coroutiner checkEnemyCrt;

    private void Awake()
    {
      // checkEnemyCrt = new Coroutiner(CheckEnemyRoutine);
    }

    private void Start()
    {
      OnSceneChanged();
    }

    public void OnSceneChanged()
    {
      controller = FindObjectOfType<StageController>();
      MoveTo(controller.mainRoom);
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
          true  => room.startPosition.position,
          false => room.linkPositions.Find(t => t.name == link).position
        };

        room.OnEntered();
      }
      else
        throw new Exception($"invalid room name: {roomName}");
    }

    public void LoadCurrentStage()
      => LoadStageFromSceneName(SceneManager.GetActiveScene().name);

    public void LoadStageFromSceneName(string sceneName)
    {
      new SceneLoader(sceneName)
       .Out(Transitions.FADEOUT, 2f)
       .In(Transitions.FADEIN, 2f)
       .OnEndOut(() => GameManager.Pool.ClearPools())
       .OnStartIn(OnSceneChanged)
       .Load();
    }
  }
}
