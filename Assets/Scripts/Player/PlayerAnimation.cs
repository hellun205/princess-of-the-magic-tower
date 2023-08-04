using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState
{
  idle, walk, run, dash
}

public class PlayerAnimation : MonoBehaviour
{
  MoveState moveState;

  Animator anim;

  private void Awake()
  {
	anim = GetComponent<Animator>();
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Idle()
  {
	anim.SetInteger("MoveState", 0);
	moveState = MoveState.idle;
  }

  public void Walk()
  {
	anim.SetInteger("MoveState", 1);
	moveState = MoveState.walk;
  }

  public void Dash()
  {
	anim.SetInteger("MoveState", 2);
	moveState = MoveState.dash;
  }
}
