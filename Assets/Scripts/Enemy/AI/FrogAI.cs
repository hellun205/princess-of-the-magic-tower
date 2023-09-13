using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEditor.Rendering;
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
        ResetCooltime();
        
        animator.SetTrigger("isJump");
        StartCoroutine(Jump(DiscriminatePlayerDir()));
        canJump = false;
      }
    }

    private void ResetCooltime()
    {
      currentCoolTime = maxCoolTime;
    }

    private Vector2 DiscriminatePlayerDir()
    {
      Vector2 dir;
      
      if (transform.position.x <= player.transform.position.x) 
      {
        dir.x = 0; //왼쪽
      }
      else
      {
        dir.x = 1;
      }
      
      if (transform.position.y <= player.transform.position.y) 
      {
        dir.y = 0; // 아래
      }
      else
      {
        dir.y = 1;
      }

      return dir;
    }
    
    private IEnumerator Jump(Vector2 dir)
    {
      Vector3 randomPosition = Random.insideUnitCircle * jumpRad;
      
      while (Mathf.Abs((transform.position - randomPosition).magnitude) <= 4f)
      {
        randomPosition = Random.insideUnitCircle * jumpRad;
      }

      Debug.Log(randomPosition);
      
      randomPosition.x = transform.position.x;
      
      switch (dir.x)
      {
        case 0:
          randomPosition.x += 5f;
          break;
        case 1:
          randomPosition.x -= 5f; 
          break;
      }

      switch (dir.y)
      {
        case 0:
          randomPosition.y += 5f;
          break;
        case 1:
          randomPosition.y -= 5f; 
          break;
      }
      
      for (int i = 0; i < 60; i++)
      {
        transform.position = Vector2.Lerp(transform.position, randomPosition, jumpPower * Time.deltaTime);
        
        yield return new WaitForSeconds(0.01f);

        if ((transform.position - randomPosition).magnitude <= 0.2f)
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
