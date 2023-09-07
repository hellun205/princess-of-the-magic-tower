using System.Collections;
using Managers;
using Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
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
      // deathCrt.Start();
      // transform.position = GameManager.Map.currentRoom.startPosition.position;
      var curScene = SceneManager.GetActiveScene().name;
      // GameManager.Scene.Load(curScene, Transitions.OUT, new (Transitions.FADEIN, delay: 5f), smoothPause: false);
      new SceneLoader(curScene)
       .In(Transitions.FADEIN, delay:2f)
       .Load();
      // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        // GameManager.Scene.Load("Test", new TransitionOption(Transitions.FADEOUT, 2),
        //   new TransitionOption(Transitions.FADEIN, 2), smoothPause: true);
        new SceneLoader("Test")
         .Out(Transitions.FADEOUT, 2f)
         .In(Transitions.FADEIN, 2f)
         .PauseOnTransitioning(5f)
         .Load();
      }
    }
  }
}
