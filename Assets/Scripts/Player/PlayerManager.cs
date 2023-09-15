using System;
using Interact;
using Managers;
using Scene;
using UnityEngine;
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

    private void Awake()
    {
      animation = GetComponent<PlayerAnimation>();
      skill = GetComponent<PlayerSkill>();
      playerMove = GetComponent<PlayerMove>();
      move = GetComponent<PlayerMove>();
      light = GetComponent<PlayerLight>();
      interacter = GetComponent<Interacter>();

      DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.T))
      {
        testMode = !testMode;
        Debug.Log($"테스트 모드 {testMode}.");
      }
    }

    public void Death()
    {
      GameManager.Map.currentRoom.OnExited();
      GameManager.Map.LoadCurrentStage(Transitions.OUT, new(Transitions.FADEIN, delay: 1.5f));
      GameManager.Player.light.SetDefault();
    }

    protected override void OnInteract(GameObject caster)
    {
      Death();
    }
  }
}