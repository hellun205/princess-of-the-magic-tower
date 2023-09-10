using System;
using System.Collections;
using Managers;
using UnityEngine;
using DG.Tweening;

namespace Enemy.AI
{
  public class BladeAI : EnemyAI
  {
    public bool debugMode;
    public float destroyLevel;

    [Header("Value")]
    public bool awake;
    public bool canAttack;
    public bool isAttacking;
    
    public float moveSpeed;
    public float findRad;

    public float attackCooltime;

    public GameObject targetObj;
    public LayerMask playerMask;

    public GameObject attackArea;
    
    [Header("Tween")]
    public float readyTime;
    public float attackDuration;
    public float rotateSpeed;
    
    public GameObject warningArea;

    public GameObject axe_Object;
    
    
    private void Start()
    {
      targetObj = GameManager.Player.gameObject;
    }

    private void Update()
    {
      if (!awake) return;
      
      MoveToPlayer();
      FindPlayer();
    }

    public override void StartAI()
    {
      awake = true;
    }

    private void MoveToPlayer()
    {
      Vector2 direction = targetObj.transform.position - this.transform.position;

      transform.position =
        Vector2.MoveTowards(transform.position, targetObj.transform.position, 
          moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
      if (!debugMode) return;

      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(transform.position, findRad);
    }

    private void FindPlayer()
    {
      if (!canAttack) return;

      Collider2D[] findColliders = Physics2D.OverlapCircleAll(transform.position, findRad, playerMask);

      foreach (Collider2D col in findColliders)
      {
        StartCoroutine(AttackCoroutine());
        canAttack = false;
      }
    }

    IEnumerator AttackCoroutine()
    {
      float originSpeed = moveSpeed;

      moveSpeed -= 2f;

      warningArea.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 80);
      warningArea.transform.DOScale(0.97f, 0.5f);

      yield return new WaitForSeconds(1.3f);

      attackArea.SetActive(true);
      isAttacking = true;
      
      axe_Object.transform.transform
        .DORotate(new Vector3(0, 0, 360) * rotateSpeed, attackDuration, RotateMode.FastBeyond360)
        .SetEase(Ease.Linear);
      //.SetLoops();

      yield return new WaitForSeconds(2.5f);

      attackArea.SetActive(false);

      moveSpeed = originSpeed;
      isAttacking = false;

      warningArea.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
      yield return new WaitForSeconds(0.5f);
      warningArea.transform.DOScale(0f, 1f);

      yield return new WaitForSeconds(attackCooltime);

      canAttack = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
      if (!isAttacking) return;

      if (other.transform.CompareTag("Player"))
      {
        EnemyController.AttackPlayer();
      }
      else if(other.transform.CompareTag("Obstacle"))
      {
        var obstacle = other.GetComponent<Obstacle>();

        if(obstacle.destroyLevel <= destroyLevel)
        {
          obstacle.DecreaseHP();
          Debug.Log($"{gameObject.name} hitted {obstacle.gameObject.name}");
        }
      }
    }
  }
}
