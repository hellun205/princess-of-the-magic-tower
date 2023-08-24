using Map;
using Player;
using Scene;

namespace Managers
{
  public class GameManager : MonoBehaviourSingleton<GameManager>, IDontDestoryObject
  {
    public static MapManager Map { get; private set; }
    public static PlayerManager Player { get; private set; }
    public static SceneController Scene { get; private set; }
    public static Transition Transition { get; private set; }

    protected override void Awake()
    {
      base.Awake();
      
      Map = FindObjectOfType<MapManager>();
      Player = FindObjectOfType<PlayerManager>();
      Scene = FindObjectOfType<SceneController>();
      Transition = FindObjectOfType<Transition>();
    }
  }
}
