using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Managers;
using UI;
using UnityEngine;

namespace Player
{
  public class PlayerMove : MonoBehaviour
  {
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float dashSpeed;

    [SerializeField]
    private Vector2 inputVec;

    [SerializeField]
    private bool isDashing;

    [SerializeField]
    private bool canMove = true;

    public GameObject dashDummy;

    private PlayerAnimation thePlayerAnimation;
    private PlayerManager thePlayerManager;
    
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    private List<int> attackedList = new();

    private JoyStick moveJoyStick;

    private void Awake()
    {
      thePlayerAnimation = GetComponent<PlayerAnimation>();
      thePlayerManager = GetComponent<PlayerManager>();

      rigid = GetComponent<Rigidbody2D>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      moveJoyStick = GameManager.ManagedObject.Find<JoyStick>("$joystick_move");
      moveJoyStick.onValueChange += MoveJoyStick_OnValueChange;
    }

    private void MoveJoyStick_OnValueChange(Vector2 value)
    {
      inputVec = value;
      Debug.Log(value);
    }

    // Update is called once per frame
    private void Update()
    {
      // inputVec.x = Input.GetAxisRaw("Horizontal");
      // inputVec.y = Input.GetAxisRaw("Vertical");

      var h = Input.GetAxisRaw("Horizontal");
      var v = Input.GetAxisRaw("Vertical");

      if (!moveJoyStick.isUsing)
        moveJoyStick.value = new Vector2(h, v).normalized;

      Flip();
      SetAnim();
    }

    private void FixedUpdate()
    {
      Move();
    }

    private void Move()
    {
      if (isDashing || !canMove) return;
      Vector2 nextVec = inputVec * moveSpeed * Time.fixedDeltaTime;
      rigid.MovePosition(rigid.position + nextVec);
      // transform.Translate(nextVec);
    }

    private void Flip()
    {
      if (inputVec.x > 0)
      {
        spriteRenderer.flipX = true;
      }
      else if (inputVec.x < 0)
      {
        spriteRenderer.flipX = false;
      }
    }

    public void Dash()
    {
      StartCoroutine(DashCoroutine());
      thePlayerAnimation.SetState(MoveState.dash);
    }

    IEnumerator DashCoroutine()
    {
      StartCoroutine(SpawnDashSprite());

      attackedList.Clear();
      isDashing = true;
      //spriteRenderer.color = Color.cyan;

      rigid.AddForce(inputVec * dashSpeed, ForceMode2D.Impulse);

      yield return new WaitForSeconds(0.13f);
      rigid.velocity = Vector2.zero;

      yield return new WaitForSeconds(0.1f);
      //spriteRenderer.color = Color.white;

      thePlayerAnimation.SetState(MoveState.idle);
      isDashing = false;
    }

    IEnumerator SpawnDashSprite()
    {
      int count = 0;

      for (int i = 0; i < 15; i++)
      {
        yield return new WaitForSeconds(0.01f);

        GameObject dummy = Instantiate(dashDummy, transform.position, Quaternion.identity);

        Color color = count switch
        {
          0 => new Color32(120, 246, 255, 255),
          1 => new Color32(120, 246, 255, 255),
          2 => new Color32(55, 159, 255, 255),
          3 => new Color32(55, 159, 255, 255),
          4 => new Color32(54, 86, 255, 255),
          5 => new Color32(60, 54, 255, 255),
          6 => new Color32(118, 54, 255, 255),
          7 => new Color32(152, 54, 255, 255),
          _ => new Color32(183, 54, 239, 255),
        };

        dummy.GetComponent<SpriteRenderer>().color = color;
        count++;

        // if (count == 9)
        //   count = 8;
      }
    }

    private void SetAnim()
    {
      if (isDashing) return;

      if (inputVec.Equals(Vector2.zero))
      {
        thePlayerAnimation.SetState(MoveState.idle);
      }
      else
      {
        thePlayerAnimation.SetState(MoveState.walk);
      }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
      // if (!isDashing && other.CompareTag("Enemy")) 
      //   thePlayerManager.Death();
      
      if (!isDashing || !other.CompareTag("Enemy")) return;
      var enemy = other.GetComponent<EnemyController>();

      if (attackedList.Contains(enemy.pool.index)) return;
      attackedList.Add(enemy.pool.index);

      enemy.Hit(1);
    }
  }
}
