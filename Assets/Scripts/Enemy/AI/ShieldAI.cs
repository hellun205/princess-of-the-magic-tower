using System;
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy.AI
{
  public class ShieldAI : EnemyAI
  {
    public bool debugMode;

    [Header("Value")]
    public bool awake;

    public bool targetFind;

    [Range(0, 10)]
    public float rotateSpeed;

    public float lookDistance;
    public float stopDistance;

    private Vector2 destination;

    public GameObject targetObj;

    [Header("Dash")]
    public bool canDash;
    // private bool isDashing = false;

    [NonSerialized]
    private bool touchingObstacle = false;

    [Range(0, 10)]
    public float dashSpeed;

    public Transform dashPosition;

    public float maxCoolTime;
    private float currentCoolTime;

    public float dashReadyTime;

    [Header("Mask")]
    public LayerMask targetMask;

    public LayerMask stopMask;

    private CapsuleCollider2D capsule2D;
    private EnemyController enemyController;
    private Rigidbody2D rigid2D;

    public override void StartAI()
    {
      awake = true;
    }

    protected override void Awake()
    {
      base.Awake();
      capsule2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
      currentCoolTime = maxCoolTime;

      targetObj = GameManager.Player.gameObject;
    }

    private void Update()
    {
      if (!awake) return;

      FindPlayer();
      DecreaseDashTime();
      FollowPlayer();
    }

    private void FixedUpdate()
    {
      if (!awake) return;

      Rotate();
    }

    public void DecreaseDashTime()
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
      if (GameManager.Player is null)
      {
        return;
      }

      //enemyController.rigidbody.velocity = new Vector2(transform.position.x + )
    }

    private void Rotate()
    {
      if (targetFind) return;

      transform.Rotate(Vector3.forward * rotateSpeed);
    }

    private void OnDrawGizmos()
    {
      if (!debugMode) return;

      Debug.DrawRay(transform.position, transform.right * lookDistance);
    }

    private void FindPlayer()
    {
      if (!canDash)
      {
        Debug.DrawRay(transform.position, transform.right * lookDistance, Color.red);
        return;
      }

      Debug.DrawRay(transform.position, transform.right * lookDistance);

      RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, lookDistance, targetMask);

      if (hit)
      {
        targetFind = true;

        StartCoroutine(Dash());
      }
    }

    private IEnumerator Dash()
    {
      canDash = false;
      var targetVec = dashPosition.position;

      yield return new WaitForSeconds(0.45f);
      isAttacking = true;

      while (isAttacking)
      {
        Debug.DrawRay(transform.position, transform.right * stopDistance, Color.magenta);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, capsule2D.bounds.size, 0f, transform.right,
          stopDistance, targetMask);

        if (hit) yield return false;

        transform.Translate(Vector3.right * (Time.deltaTime * (dashSpeed * 10)));
        yield return new WaitForEndOfFrame();
      }

      ResetCooltime();
      isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        ResetCooltime();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
      if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        isAttacking = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
      if (!isAttacking) return;

      if (other.CompareTag("Player"))
      {
        EnemyController.AttackPlayer();
      }
    }
  }
}
