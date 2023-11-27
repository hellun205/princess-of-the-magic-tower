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

    [Header("Value")] public bool awake;

    public bool targetFind;
    private bool canMove = true;

    private Vector3 destPos;

    protected override bool forceAttack => true;

    public LayerMask obstacleMask;

    [Range(0, 10)] public float moveSpeed;
    public float lookDistance;

    public GameObject bodySprite;

    [Header("Dash")] public bool canDash;
    [Range(0, 1)] public float dashSpeed;

    public float maxCoolTime;
    private float currentCoolTime;

    [Header("Mask")] public LayerMask targetMask;

    private EnemyController enemyController;
    private Rigidbody2D rigid2D;
    private Animator animator;
    private CapsuleCollider2D collider;

    public override void StartAI()
    {
      awake = true;
      canMove = true;
      currentCoolTime = maxCoolTime;
    }

    protected override void Awake()
    {
      base.Awake();
      animator = GetComponentInChildren<Animator>();
      collider = GetComponentInChildren<CapsuleCollider2D>();
    }

    private void Start()
    {
      currentCoolTime = maxCoolTime;
      target = GameManager.Player.transform;

      canMove = true;
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
      if (GameManager.Player is null || canMove == false)
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
      float scale = type ? -0.8f : 0.8f;

      bodySprite.transform.localScale =
        new Vector3(scale, bodySprite.transform.localScale.y, bodySprite.transform.localScale.z);
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

      if (distance <= 2f)
      {
        if (targetFind) return;

        targetFind = true;

        StartCoroutine(Dash());
        destPos = target.transform.position;
      }
    }

    private IEnumerator Dash()
    {
      canMove = false;
      canDash = false;
      animator.SetInteger("animState", 2);

      yield return new WaitForSeconds(0.45f);
      isAttacking = true;

      RaycastHit2D hit;

      while (isAttacking)
      {
        if ((transform.position - destPos).magnitude <= 1f) break;


        transform.DOMove(destPos, dashSpeed, false);
        yield return new WaitForEndOfFrame();
      }

      ResetCooltime();
      isAttacking = false;

      yield return new WaitForSeconds(0.5f);

      canMove = true;
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