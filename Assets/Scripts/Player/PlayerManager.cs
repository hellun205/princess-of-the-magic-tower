using System;
using Managers;
using Scene;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Player
{
  public class PlayerManager : MonoBehaviour
  {
    public bool testMode;

    public PlayerSkill skill;
    private PlayerMove playerMove;
    public new PlayerAnimation animation;
    private Coroutiner deathCrt;
    public PlayerMove move;
    public new PlayerLight light;

    private void Awake()
    {
      animation = GetComponent<PlayerAnimation>();
      skill = GetComponent<PlayerSkill>();
      playerMove = GetComponent<PlayerMove>();
      move = GetComponent<PlayerMove>();
      light = GetComponent<PlayerLight>();

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
      GameManager.Map.LoadCurrentStage(Transitions.OUT, new(Transitions.FADEIN, delay: 1.5f));
    }
  }
}