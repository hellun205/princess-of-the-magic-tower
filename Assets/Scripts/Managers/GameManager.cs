using System.Linq;
using AYellowpaper.SerializedCollections;
using Dialogue;
using Map;
using Player;
using Pool;
using Ranking;
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

    public static AudioManager audioManager { get; private set; }

    public SerializedDictionary<ProfileImage, Sprite> profiles;

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

    public StopwatchObject stopwatchObject;

    public static int death;
    public static float record;
    public static ProfileImage profile;

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
      audioManager = FindObjectOfType<AudioManager>();
    }

    private void Awake()
    {
      Application.targetFrameRate = 60;
      
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
        position = PlayerLocation.GetPositionInRoom(),
        cleared = FindObjectsOfType<Room>().Where(x => x.isCleared).Select(x => x.name).ToArray(),
        objectName = objectName,
        nickname = nickname,
        death = death,
        record = stopwatchObject.elapsed,
        profile = profile
      };

      PlayerPrefs.SetString("save", JsonUtility.ToJson(data));
    }

    public static SaveData LoadData()
    {
      return JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("save"));
    }

    private static bool isMainMenu;
    
    public static void Loaded(UnityEngine.SceneManagement.Scene a, LoadSceneMode b)
    {
      var data = LoadData();
      Map.moveOnStart = false;
      SceneManager.sceneLoaded -= Loaded;
      Map.ReloadStage();
      foreach (var room in FindObjectsOfType<Room>().Where(x => data.cleared.Contains(x.name)))
        room.isCleared = true;

      Map.MoveTo(data.room);
      PlayerLocation.SetPositionInRoom(data.position);
      Debug.Log(data.room);
      if (isMainMenu)
      {
        Manager.stopwatchObject.elapsed = data.record;
        nickname = data.nickname;
        profile = data.profile;
        death = data.death;
      }
    }

    public static void InitLoad(bool isMainmenu = false)
    {
      SceneManager.sceneLoaded += Loaded;
      isMainMenu = isMainmenu;
    }

    public static bool HasSave() => PlayerPrefs.HasKey("save");

    public void Clear()
    {
      GameManager.Manager.stopwatchObject.isRunning = false;

      RankingData data;
      
      if (PlayerPrefs.HasKey("ranking"))
      {
        data = JsonUtility.FromJson<RankingData>(PlayerPrefs.GetString("ranking"));
        var list = data.ranking.ToList();
        
        if (list.Exists(x => x.nickname == nickname))
        {
          var tmp = list.Single(x => x.nickname == nickname);
          list = list.Where(x => x.nickname != nickname).ToList();
          tmp.record = record;
          tmp.death = death;
          tmp.profile = profile;
          list.Add(tmp);
        }
        else
        {
          list.Add(new RankingData.Item
          {
            nickname = nickname,
            death = death,
            record = record,
            profile = profile
          });
        }
        
        data.ranking = list.ToArray();
      }
      else
      {
        data = new RankingData
        {
          ranking = new []
          {
            new RankingData.Item
            {
              nickname = nickname,
              death = death,
              record = record,
              profile = profile
            }
          }
        };
      }

      data.ranking = data.ranking.OrderBy(x => x.record).ThenBy(x => x.death).ToArray();
      
      PlayerPrefs.SetString("ranking", JsonUtility.ToJson(data));
      PlayerPrefs.DeleteKey("save");
    }
  }
}
