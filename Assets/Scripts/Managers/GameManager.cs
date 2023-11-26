using System.Linq;
using Dialogue;
using Map;
using Player;
using Pool;
using Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util;
using Touch = UI.Touch;

namespace Managers
{
  public class GameManager : MonoBehaviour
  {
    public static GameManager Manager { get; private set; }
    public static MapManager Map { get; private set; }
    public static PlayerManager Player { get; private set; }
    public static Transition Transition { get; private set; }
    public static PoolManager Pool { get; private set; }
    public static Binder ManagedObject { get; private set; }
    public static ObjectCollection Prefabs { get; private set; }
    public static DialogueManager Dialogue { get; private set; }
    public static AvatarCollection Avatars { get; private set; }
    public static CameraController Camera { get; private set; }
    public static PlayerLocation PlayerLocation { get; set; }

    private static CoroutineObject _coroutineObject;

    public static CoroutineObject CoroutineObject
    {
      get
      {
        if (_coroutineObject != null) return _coroutineObject;

        _coroutineObject = new GameObject("Coroutiner", typeof(CoroutineObject)).GetComponent<CoroutineObject>();
        DontDestroyOnLoad(_coroutineObject.gameObject);
        return _coroutineObject;
      }
    }

    public delegate void GameManagerEventListener();

    public static event GameManagerEventListener OnLoaded;

    public static string nickname;

    private void Init()
    {
      Manager = this;
      Map = FindObjectOfType<MapManager>();
      Player = FindObjectOfType<PlayerManager>();
      Transition = new Transition();
      Pool = new PoolManager();
      ManagedObject = new Binder(this).Bind('$');
      Prefabs = GameObject.Find("@prefab_objects").GetComponent<ObjectCollection>();
      Dialogue = FindObjectOfType<DialogueManager>();
      Avatars = FindObjectOfType<AvatarCollection>();
      Camera = FindObjectOfType<CameraController>();
    }

    private void Awake()
    {
      DontDestroyOnLoad(gameObject);
      Init();
      OnLoaded?.Invoke();
    }

    private void Start()
    {
      ManagedObject.Get("menu_panel").SetActive(false);
      ManagedObject.Get<Button>("menu_button").onClick.AddListener(OnMenuButtonClick);
      ManagedObject.Get<Touch>("menu_panel").onTouch += e => CloseMenu();
      ManagedObject.Get<Button>("menu_btn_setting").onClick.AddListener(OpenSetting);
      ManagedObject.Get<Button>("menu_btn_continue").onClick.AddListener(CloseMenu);
      ManagedObject.Get<Button>("menu_btn_title").onClick.AddListener(GoToTitle);
    }

    private void OnMenuButtonClick()
    {
      Utils.Pause();
      ManagedObject.Get("menu_panel").SetActive(true);
    }

    private void CloseMenu()
    {
      Utils.UnPause();
      ManagedObject.Get("menu_panel").SetActive(false);
    }

    private void OpenSetting()
    {
    }

    private void GoToTitle()
    {
      CloseMenu();
      new SceneLoader("TitleScene")
       .Out(Transitions.FADEOUT)
       .In(Transitions.IN)
       .OnStartIn(() =>
        {
          Object.Destroy(FindObjectOfType<GameManager>().gameObject);
          // Object.Destroy(FindObjectOfType<PlayerLocation>().gameObject); 
          Object.Destroy(FindObjectOfType<PlayerManager>().gameObject);
          Object.Destroy(FindObjectOfType<CameraController>().gameObject);
        })
       .Load();
    }

    public void Save(string room, string objectName)
    {
      var data = new SaveData()
      {
        stage = SceneManager.GetActiveScene().name,
        room = room,
        position = GameManager.PlayerLocation.GetPositionInRoom(),
        cleared = FindObjectsOfType<Room>().Where(x => x.isCleared).Select(x => x.name).ToArray(),
        objectName = objectName
      };

      PlayerPrefs.SetString("save", JsonUtility.ToJson(data));
    }

    public static SaveData LoadData()
    {
      return JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("save"));
    }

    public static void Loaded(UnityEngine.SceneManagement.Scene a, LoadSceneMode b)
    {
      var data = LoadData();
      GameManager.Map.moveOnStart = false;
      SceneManager.sceneLoaded -= Loaded;
      GameManager.Map.ReloadStage();
      foreach (var room in FindObjectsOfType<Room>().Where(x => data.cleared.Contains(x.name)))
        room.isCleared = true;

      GameManager.Map.MoveTo(data.room);
      GameManager.PlayerLocation.SetPositionInRoom(data.position);
      Debug.Log(data.room);
    }

    public static void InitLoad()
    {
      SceneManager.sceneLoaded += GameManager.Loaded;
    }

    public static bool HasSave() => PlayerPrefs.HasKey("save");
  }
}
