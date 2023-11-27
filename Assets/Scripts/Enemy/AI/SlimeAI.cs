using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Enemy.AI;
using Managers;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

namespace Enemy.AI
{
  public class SlimeAI : EnemyAI
  {
    [Header("Value")] public bool awake;

    public bool debugMode;

    [Header("Jump")] public float currentCoolTime;

    public float maxCoolTime;

    public float jumpRad;
    public float jumpPower;

    public bool canJump;

    private Animator animator;
    public GameObject player;

    public GameObject bodySprite;
    
    protected override void Awake()
    {
      base.Awake();
      animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
      currentCoolTime = maxCoolTime;
      player = GameManager.Player.gameObject;
    }

    private void Update()
    {
      DecreaseCoolTime();
    }

    public override void StartAI()
    {
      awake = true;
      player = GameManager.Player.gameObject;

      animator.SetInteger("animState", 0);
      canJump = true;
    }

    private void DecreaseCoolTime()
    {
      if (!canJump || !awake) return;

      if (player.transform.position.x < transform.position.x)
      {
        FlipSprite(false);
      }
      else
      {
        FlipSprite(true);
      }
      
      currentCoolTime -= Time.deltaTime;

      if (currentCoolTime <= 0)
      {
        ResetCooltime();

        StartCoroutine(Jump(DiscriminatePlayerDir()));
        canJump = false;
      }
    }

    private void FlipSprite(bool type)
    {
      int scale = type ? -1 : 1;

      bodySprite.transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
    }
    
    private void ResetCooltime()
    {
      currentCoolTime = maxCoolTime;

      int randomTime = Random.Range(0, 2);

      if (randomTime == 0)
        currentCoolTime += Random.Range(0f, 0.8f);
      else
        currentCoolTime -= Random.Range(0f, 0.8f);
    }

    private Vector2 DiscriminatePlayerDir()
    {
      Vector2 dir;

      // int percentX = 0.35f.ApplyPercentage() ? 0 : 1;
      // int percentY = 0.35f.ApplyPercentage() ? 1 : 0;

      //Debug.Log(percentX + ", " + percentY);

      dir.x = transform.position.x <= player.transform.position.x ? 0 : 1;
      dir.y = transform.position.y <= player.transform.position.y ? 0 : 1;

      return dir;
    }

    private IEnumerator Jump(Vector2 dir)
    {
      Vector3 randomPosition = transform.position;

      int varientJump = Random.Range(1, 11);

      if (varientJump <= 3)
      {
        dir.x = Random.Range(0, 2);
        dir.y = Random.Range(0, 2);
      }

      int randomX = Random.Range(2, 6);
      int randomY = Random.Range(2, 6);

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

      animator.SetInteger("animState", 1);

      for (int i = 0; i < 60; i++)
      {
        transform.position = Vector2.Lerp(transform.position, randomPosition, jumpPower * Time.deltaTime);

        yield return new WaitForSeconds(0.01f);

        if ((transform.position - randomPosition).magnitude <= 0.2f)
          break;
      }

      canJump = true;

      yield return new WaitForSeconds(1f);
      animator.SetInteger("animState", 0);
    }

    private void OnDrawGizmos()
    {
      if (!debugMode) return;

      Gizmos.DrawWireSphere(transform.position, jumpRad);
    }

    private void Death()
    {
      animator.SetTrigger("die");
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