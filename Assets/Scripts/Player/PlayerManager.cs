using System.Collections;
using Managers;
using Scene;
using UnityEngine;
using Util;

namespace Player
{
  public class PlayerManager : MonoBehaviour
  {
    public bool testMode;

    public PlayerSkill skill;
    private PlayerMove playerMove;

    private Coroutiner deathCrt;

    private void Awake()
    {
      deathCrt = new(DeathRoutine);
      skill = GetComponent<PlayerSkill>();
      playerMove = GetComponent<PlayerMove>();
    }

    public void Death()
    {
      deathCrt.Start();
      transform.position = GameManager.Map.currentRoom.startPosition.position;
    }

    private IEnumerator DeathRoutine()
    {
      GameManager.Transition.Play(Transitions.OUT);
      yield return new WaitForSecondsRealtime(1.5f);
      GameManager.Transition.Play(Transitions.FADEIN);
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.T))
      {
        testMode = !testMode;
        Debug.Log($"테스트 모드 {testMode}.");
      }

      if (Input.GetKeyDown(KeyCode.F6))
      {
        GameManager.Scene.Load("Test", new TransitionOption(Transitions.FADEOUT, 2),
          new TransitionOption(Transitions.FADEIN, 2), smoothPause: true);
      }
    }
  }
}
