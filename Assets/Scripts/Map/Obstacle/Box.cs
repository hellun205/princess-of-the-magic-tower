using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpableObstacle : Obstacle
{
    public int hp;

    protected override void Awake()
    {
        base.Awake();
    }

    public void DecreaseHP()
    {
        hp--;

        if(hp <= 0)
        {
            OnBreak();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            DecreaseHP();
        }
    }
}
