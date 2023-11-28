using System.Collections;
using System.Collections.Generic;
using Enemy;
using Interact;
using Interact.Object;
using Managers;
using Pool.Extension;
using UI;
using UnityEngine;
using Util;

namespace Player
{
  public class PlayerMove : MonoBehaviour, IObstacleDestroyable
  {
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float baseMoveSpeed;

    [SerializeField]
    private float dashSpeed;

    [field: SerializeField]
    [field: Min(0)]
    public int destroyLevel { get; set; }

    [SerializeField]
    private Vector2 inputVec;

    public bool isDashing;
    
    public bool canMove = true;

    public GameObject dashDummy;

    private PlayerManager manager;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    private List<int> attackedList = new();

    private JoyStick moveJoyStick;
    private Vector2 lastMovePos;

    private SpriteFader spriteFader;

    private void Awake()
    {
      manager = GetComponent<PlayerManager>();

      rigid = GetComponent<Rigidbody2D>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      spriteFader = GetComponent<SpriteFader>();
    }

    private void Start()
    {
      moveJoyStick = GameManager.ManagedObject.Get<JoyStick>("joystick_move");
      moveJoyStick.onValueChange += MoveJoyStick_OnValueChange;
    }

    private void MoveJoyStick_OnValueChange(Vector2 value)
    {
      inputVec = value;
      if (value != Vector2.zero)
        lastMovePos = inputVec;
    }

    private void Update()
    {
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
      var scale = transform.localScale;
      if (inputVec.x != 0)
        transform.localScale = scale.Setter(x: Mathf.Abs(scale.x) * (inputVec.x > 0 ? -1 : 1));
    }

    public void Dash()
    {
      StartCoroutine(DashCoroutine());
      spriteFader.flip = lastMovePos.x > 0;
      spriteFader.Play();
    }

    private IEnumerator DashCoroutine()
    {
      // StartCoroutine(SpawnDashSprite());

      attackedList.Clear();
      isDashing = true;
      manager.interacter.currentCondition = InteractCondition.Attack;
      // manager.animation.SetState(MoveState.dash);
      //spriteRenderer.color = Color.cyan;

      rigid.AddForce(inputVec * dashSpeed, ForceMode2D.Impulse);

      yield return new WaitForSeconds(0.13f);
      rigid.velocity = Vector2.zero;

      yield return new WaitForSeconds(0.1f);
      //spriteRenderer.color = Color.white;

      manager.animation.SetState(MoveState.IdleFront);

      isDashing = false;
      manager.interacter.currentCondition = InteractCondition.Reach;
      manager.interacter.RemoveDetection();
    }

    private IEnumerator SpawnDashSprite()
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
      }
    }

    private void SetAnim()
    {
      if (isDashing) return;

      if (inputVec.Equals(Vector2.zero))
        manager.animation.SetState(lastMovePos.y > 0 ? MoveState.IdleBack : MoveState.IdleFront);
      else if (inputVec.y <= 0)
        manager.animation.SetState(MoveState.WalkFront);
      else if (inputVec.y > 0)
        manager.animation.SetState(MoveState.WalkBack);
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //   if (!isDashing || !other.CompareTag("Enemy")) return;
    //   var enemy = other.GetComponent<EnemyController>();
    //
    //   if (attackedList.Contains(enemy.pool.index)) return;
    //   attackedList.Add(enemy.pool.index);
    //
    //   enemy.Hit(1);
    // }
  }
}