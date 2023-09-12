using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Enemy.AI
{
  public class FrogAI : EnemyAI
  {
    [Header("Value")]
    public bool awake;
    public bool debugMode;
    
    [Header("Jump")]
    public float currentCoolTime;
    public float maxCoolTime;
    
    public float jumpRad;
    public float jumpPower;
    
    public bool canJump;

    [Header("Components")]
    private Rigidbody2D rigid;

    private Animator animator;
    public GameObject player;
    
    protected override void Awake()
    {
      base.Awake();
      rigid = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
    }

    private void Start()
    {
      currentCoolTime = maxCoolTime;
      //player = GameManager.Player.gameObject;
    }

    private void Update()
    {
      DecreaseCoolTime();
    }

    public override void StartAI()
    {
      awake = true;
    }

    private void DecreaseCoolTime()
    {
      if (!canJump) return;
      
      currentCoolTime -= Time.deltaTime;

      if (currentCoolTime <= 0)
      {
        int dir = DiscriminatePlayerDir();
        
        ResetCooltime();
        
        animator.SetTrigger("isJump");
        StartCoroutine(Jump(dir));
        canJump = false;
      }
    }

    private void ResetCooltime()
    {
      currentCoolTime = maxCoolTime;
    }

    private int DiscriminatePlayerDir()
    {
      Vector3 dir = (transform.position - player.transform.position).normalized;

      Vector3 cross = Vector3.Cross(transform.position.normalized, player.transform.position.normalized);
      
      if (cross.y < 0f) 
      {
        Debug.Log("L");
        return 0;
      }
      else
      {
        Debug.Log("R");
        return 1;
      }
    }
    
    private IEnumerator Jump(int dir)
    {
      Vector3 randomPosition = Random.insideUnitCircle * jumpRad;
      
      while (Mathf.Abs(transform.position.x - randomPosition.x) <= 1f && Mathf.Abs(transform.position.y - randomPosition.y) <= 1f)
      {
        randomPosition = Random.insideUnitCircle * jumpRad;
      }
      
      switch (dir)
      {
        case 0:
          randomPosition.x = Mathf.Abs(randomPosition.x);
          break;
        case 1:
          randomPosition.x =Mathf.Abs(randomPosition.x) * -1;
          break;
      }
      
      for (int i = 0; i < 60; i++)
      {
        transform.position = Vector2.Lerp(transform.position, randomPosition, jumpPower);
        
        yield return new WaitForSeconds(0.01f);

        if ((transform.position - randomPosition).magnitude <= 1f)
          break;
      }

      canJump = true;
    }

    private void OnDrawGizmos()
    {
      if (!debugMode) return;
      
      Gizmos.DrawWireSphere(transform.position, jumpRad);
    }
  }
}
