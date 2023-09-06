using System.Collections;
using Managers;
using Scene;
using UnityEngine;
using Util;

namespace Player
{
  public class PlayerManager : MonoBehaviour
  {
    public PlayerSkill skill;
    
    private Coroutiner deathCrt;

    private void Awake()
    {
      deathCrt = new(DeathRoutine);
      skill = GetComponent<PlayerSkill>();
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
      if (Input.GetKeyDown(KeyCode.F6))
      {
        GameManager.Scene.Load("Test", new TransitionOption(Transitions.FADEOUT, 2),
          new TransitionOption(Transitions.FADEIN, 2), slowly: true);
      }
    }
  }
}
