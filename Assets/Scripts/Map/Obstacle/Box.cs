using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Obstacle
{
  public int destroyLevel;

  protected override void Awake()
  {
    base.Awake();
  }

  public void DecreaseHP()
  {
    destroyLevel--;

    if (destroyLevel <= 0)
    {
      OnBreak();
    }
  }


  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Enemy"))
    {
      DecreaseHP();
    }
  }
}
