using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Enemy.AI;
using Managers;
using UnityEngine;

public class FollowAI : EnemyAI
{
  [Header("Value")]
  public bool awake;
  public bool debugMode;

  public float moveSpeed;

  [Header("Scripts")]
  public GameObject player;
  
  public override void StartAI()
  {
    awake = true;
    player = GameManager.Player.gameObject;
  }

  private void Start()
  {
    //player = GameManager.Player.gameObject;
  }

  private void Update()
  {
    if (!awake) return;
    
    PlayerFollow();
  }

  private void PlayerFollow()
  {
    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 
      moveSpeed * Time.deltaTime);
  }
  
  private void OnTriggerStay2D(Collider2D other)
  {

  }
}
