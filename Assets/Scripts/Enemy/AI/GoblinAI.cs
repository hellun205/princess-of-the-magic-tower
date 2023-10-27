using System;
using System.Collections;
using DG.Tweening;
using Managers;
using UnityEngine;
using Util;

namespace Enemy.AI
{
  public class GoblinAI : EnemyAI
  {
    public bool debugMode;

    public Transform target;

    [Header("Value")] 
    public bool awake;

    public bool targetFind;
    private Vector3 destPos;

    [Range(0, 10)]

    public float moveSpeed;
    public float lookDistance;

    public GameObject bodySprite;

    [Header("Dash")] 
    public bool canDash;
    [Range(0, 1)] public float dashSpeed;

    public float maxCoolTime;
    private float currentCoolTime;

    [Header("Mask")] 
    public LayerMask targetMask;
    
    private EnemyController enemyController;
    private Rigidbody2D rigid2D;
    private Animator animator;

    public override void StartAI()
    {
      awake = true;
    }

    protected override void Awake()
    {
      base.Awake();
      animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
      currentCoolTime = maxCoolTime;
      target = GameManager.Player.transform;
    }

    private void Update()
    {
      if (!awake) return;

      DecreaseDashTime();
      FollowPlayer();
    }

    private void DecreaseDashTime()
    {
      if (isAttacking) return;

      currentCoolTime -= Time.deltaTime;

      if (currentCoolTime <= 0)
      {
        currentCoolTime = maxCoolTime;
        canDash = true;
        targetFind = false;
      }
    }

    private void ResetCooltime()
    {
      currentCoolTime = maxCoolTime;
    }

    private void FollowPlayer()
    {
      if (GameManager.Player is null || canDash == false)
      {
        return;
      }

      MoveToPlayer();
      DiscriminateTargetDistance();
    }

    private void MoveToPlayer()
    {
      animator.SetInteger("animState", 1);

      if (target.position.x < transform.position.x)
      {
        FlipSprite(false);
      }
      else
      {
        FlipSprite(true);
      }

      transform.position =
        Vector2.MoveTowards(transform.position, target.position,
          moveSpeed * Time.deltaTime);
    }

    private void FlipSprite(bool type)
    {
      int scale = type ? -1 : 1;

      bodySprite.transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmos()
    {
      if (!debugMode) return;

      Debug.DrawRay(transform.position, transform.right * lookDistance);
    }

    private void DiscriminateTargetDistance()
    {
      if (!canDash)
      {
        Debug.DrawRay(transform.position, transform.right * lookDistance, Color.red);
        return;
      }

      float distance = (target.position - transform.position).magnitude;

      if (distance <= 3f)
      {
        if (targetFind) return;

        targetFind = true;

        StartCoroutine(Dash());
        destPos = target.transform.position;
        Debug.Log(destPos);
      }
    }

    private IEnumerator Dash()
    {
      canDash = false;
      animator.SetInteger("animState", 2);

      yield return new WaitForSeconds(0.45f);
      isAttacking = true;

      while (isAttacking)
      {
        if ((transform.position - destPos).magnitude <= 2f) break;

        transform.DOMove(destPos, dashSpeed, false);
        yield return new WaitForEndOfFrame();
      }

      ResetCooltime();
      isAttacking = false;
    }

    public void Death()
    {
      animator.SetTrigger("die");
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
      if (other.gameObject.layer.CheckLayer("Obstacle", "Wall"))
        ResetCooltime();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
      if (other.gameObject.layer.CheckLayer("Obstacle", "Wall"))
        isAttacking = false;
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //   if (!isAttacking) return;
    //
    //   if (other.CompareTag("Player"))
    //   {
    //     EnemyController.AttackPlayer();
    //   }
    // }
  }
}