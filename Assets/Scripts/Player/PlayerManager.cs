using Managers;
using Scene;
using UnityEngine;

namespace Player
{
  public class PlayerManager : MonoBehaviour
  {
    public new PlayerAnimation animation;
    public PlayerSkill skill;
    public PlayerMove move;

    private void Awake()
    {
      animation = GetComponent<PlayerAnimation>();
      skill = GetComponent<PlayerSkill>();
      move = GetComponent<PlayerMove>();

      DontDestroyOnLoad(gameObject);
    }

    public void Death()
    {
      GameManager.Map.LoadCurrentStage(Transitions.OUT, new(Transitions.FADEIN, delay: 1.5f));
    }
  }
}