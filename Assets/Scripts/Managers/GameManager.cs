using Dialogue;
using Map;
using Player;
using Pool;
using Scene;
using UnityEngine;

namespace Managers
{
  public class GameManager : MonoBehaviour
  {
    public static GameManager Manager { get; private set; }
    public static MapManager Map { get; private set; }
    public static PlayerManager Player { get; private set; }
    public static SceneController Scene { get; private set; }
    public static Transition Transition { get; private set; }
    public static PoolManager Pool { get; private set; }
    public static ManagedObjectManager ManagedObject { get; private set; }
    public static ObjectCollection Prefabs { get; private set; }
    public static DialogueManager Dialogue { get; private set; }
    public static AvatarCollection Avatars { get; private set; }

    private void Awake()
    {
      Manager = this;
      Map = FindObjectOfType<MapManager>();
      Player = FindObjectOfType<PlayerManager>();
      Scene = FindObjectOfType<SceneController>();
      Transition = FindObjectOfType<Transition>();
      Pool = PoolManager.Instance;
      ManagedObject = FindObjectOfType<ManagedObjectManager>();
      Prefabs = GameObject.Find("@prefab_objects").GetComponent<ObjectCollection>();
      Dialogue = FindObjectOfType<DialogueManager>();
      Avatars = FindObjectOfType<AvatarCollection>();
    }
  }
}
