using Camera;
using Dialogue;
using Map;
using Player;
using Pool;
using Scene;
using UnityEngine;
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

    public delegate void GameManagerEventListener();

    public static event GameManagerEventListener OnLoaded;

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
      Utils.Pause(true);
      ManagedObject.Get("menu_panel").SetActive(true);
    }

    private void CloseMenu()
    {
      Utils.UnPause(true);
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
       .In(Transitions.FADEIN)
       .Load();
    }
  }
}