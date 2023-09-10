using Managers;
using Scene;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Player
{
  public class PlayerManager : MonoBehaviour
  {
    public new PlayerAnimation animation;
    public PlayerSkill skill;
    private Coroutiner deathCrt;
    public PlayerMove move;
    public new PlayerLight light;

    private void Awake()
    {
      animation = GetComponent<PlayerAnimation>();
      skill = GetComponent<PlayerSkill>();
      move = GetComponent<PlayerMove>();
      light = GetComponent<PlayerLight>();

      DontDestroyOnLoad(gameObject);
    }

    public void Death()
    {
      GameManager.Map.LoadCurrentStage(Transitions.OUT, new(Transitions.FADEIN, delay: 1.5f));
    }
  }
}