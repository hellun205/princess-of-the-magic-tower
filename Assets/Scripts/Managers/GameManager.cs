using Map;
using Player;

namespace Managers
{
  public class GameManager : MonoBehaviourSingleton<GameManager>, IDontDestoryObject
  {
    public static MapManager Map { get; private set; }
    public static PlayerManager Player { get; private set; }

    protected override void Awake()
    {
      base.Awake();
      
      Map = FindObjectOfType<MapManager>();
      Player = FindObjectOfType<PlayerManager>();
    }
  }
}
