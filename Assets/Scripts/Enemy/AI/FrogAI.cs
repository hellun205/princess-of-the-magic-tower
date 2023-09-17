using System.Collections;
using Managers;
using UnityEngine;
using Util;
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
      player = GameManager.Player.gameObject;

      animator.SetTrigger("isIdle");
      canJump = true;
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

      int percentX = 0.35f.ApplyPercentage() ? 0 : 1;
      int percentY = 0.35f.ApplyPercentage() ? 1 : 0;

      dir.x = transform.position.x <= player.transform.position.x ? percentX : percentY;
      dir.y = transform.position.y <= player.transform.position.y ? percentX : percentY;


      return dir;
    }

    private IEnumerator Jump(Vector2 dir)
    {
      Vector3 randomPosition = Random.insideUnitCircle * jumpRad;

      while (Mathf.Abs((transform.position - randomPosition).magnitude) <= 4f)
      {
        randomPosition = Random.insideUnitCircle * jumpRad;
      }

      randomPosition = transform.position;

      int randomX = Random.Range(2, 5);
      int randomY = Random.Range(2, 5);


      randomPosition.x = dir.x switch
      {
        0 => randomPosition.x + randomX,
        1 => randomPosition.x - randomX,
        _ => 0,
      };

      randomPosition.y = dir.y switch
      {
        0 => randomPosition.y + randomY,
        1 => randomPosition.y - randomY,
        _ => 0,
      };


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

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //   // if (!isAttacking) return;
    //
    //   if (other.transform.CompareTag("Player"))
    //   {
    //     EnemyController.AttackPlayer();
    //   }
    // }
  }
}
