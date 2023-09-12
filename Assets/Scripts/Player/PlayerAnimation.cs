using System;
using UnityEngine;

namespace Player
{
  public class PlayerAnimation : MonoBehaviour
  {
    [NonSerialized]
    public MoveState currentState;

    private Animator anim;

    private void Awake()
    {
      anim = GetComponent<Animator>();
    }

    public void SetState(MoveState state)
    {
      anim.SetInteger("MoveState", (int)state);
      currentState = state;
    }
  }
}
