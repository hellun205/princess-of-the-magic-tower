using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Enemy.AI;

public class Box : Obstacle
{
  protected override void Awake()
  {
    base.Awake();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.transform.CompareTag("Player"))
    {
      var player = collision.transform.GetComponent<PlayerMove>();

      if (player.destroyLevel >= destroyLevel && player.isDashing)
      DecreaseHP();
    }
  }
}
