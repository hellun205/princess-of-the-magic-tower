using System;
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
    public static SceneController Scene { get; private set; }
    public static Transition Transition { get; private set; }

    public static PoolManager Pool { get; private set; }

    // public static ManagedObjectManager ManagedObject { get; private set; }
    public static Binder ManagedObject { get; private set; }
    public static ObjectCollection Prefabs { get; private set; }
    public static DialogueManager Dialogue { get; private set; }
    public static AvatarCollection Avatars { get; private set; }

    private void Init()
    {
      Manager = this;
      Map = FindObjectOfType<MapManager>();
      Player = FindObjectOfType<PlayerManager>();
      Scene = FindObjectOfType<SceneController>();
      Transition = FindObjectOfType<Transition>();
      Pool = new PoolManager();
      // ManagedObject = FindObjectOfType<ManagedObjectManager>();
      ManagedObject = new Binder().Bind('$');
      Prefabs = GameObject.Find("@prefab_objects").GetComponent<ObjectCollection>();
      Dialogue = FindObjectOfType<DialogueManager>();
      Avatars = FindObjectOfType<AvatarCollection>();
    }

    private void Awake()
    {
      Init();
    }

    private void Start()
    {
      ManagedObject.Get("menu_panel").SetActive(false);
      ManagedObject.Get<Button>("menu_button").onClick.AddListener(OnMenuButtonClick);
      ManagedObject.Get<Touch>("menu_panel").onClick.AddListener(e => CloseMenu());
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
      Scene.Load("TitleScene", Transitions.FADEOUT, Transitions.IN);
    }
  }
}