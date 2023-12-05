using System;
using System.Collections;
using System.Linq;
using Interact;
using Managers;
using Map;
using Map.Door;
using Scene;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Util;

namespace Player
{
  public class PlayerManager : InteractiveObject
  {
    public bool testMode;

    [NonSerialized]
    public PlayerSkill skill;

    [NonSerialized]
    private PlayerMove playerMove;

    [NonSerialized]
    public new PlayerAnimation animation;

    [NonSerialized]
    private Coroutiner deathCrt;

    [NonSerialized]
    public PlayerMove move;

    [NonSerialized]
    public new PlayerLight light;

    [NonSerialized]
    public Interacter interacter;

    private GameObject testModeText;

    private bool isDeath;

    private void Awake()
    {
      animation = GetComponent<PlayerAnimation>();
      skill = GetComponent<PlayerSkill>();
      playerMove = GetComponent<PlayerMove>();
      move = GetComponent<PlayerMove>();
      light = GetComponent<PlayerLight>();
      interacter = GetComponent<Interacter>();
      GameManager.OnLoaded += () =>
      {
        testModeText = GameManager.ManagedObject.Get("testmode");
        testModeText.SetActive(testMode);
      };

      DontDestroyOnLoad(gameObject);
      SceneManager.sceneLoaded += ((arg0, mode) =>
      {
        if (isDeath)
          isDeath = false;
      });
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.T))
      {
        testMode = !testMode;
        testModeText.SetActive(testMode);
      }
    }

    public void Death()
    {
      if (testMode || isDeath) return;
      StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(0.22f);
        isDeath = true;

        GameManager.Map.currentRoom.OnExited();
        if (GameManager.HasSave())
        {
            var data = GameManager.LoadData();

            GameManager.InitLoad();
            GameManager.Map.LoadStageFromSceneName(data.stage, Transitions.OUT, new(Transitions.FADEIN, delay: 1.5f));
        }
        else
            GameManager.Map.LoadCurrentStage(Transitions.OUT, new(Transitions.FADEIN, delay: 1.5f));

        light.SetDefault();
        GameManager.death++;
        GameManager.Manager.stopwatchObject.isRunning = false;
    }
    
    protected override void OnInteract(GameObject caster)
    {
      Death();
    }
  }
}
