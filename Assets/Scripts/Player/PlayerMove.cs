using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
  [SerializeField] private float moveSpeed;
  [SerializeField] private float dashSpeed;
  [SerializeField] private Vector2 inputVec;

  [SerializeField] private bool isDashing;
  [SerializeField] private bool canMove = true;

  public GameObject dashDummy;

  PlayerAnimation thePlayerAnimation;

  Rigidbody2D rigid;
  SpriteRenderer spriteRenderer;

  private void Awake()
  {
	thePlayerAnimation = GetComponent<PlayerAnimation>();

	rigid = GetComponent<Rigidbody2D>();
	spriteRenderer = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
	inputVec.x = Input.GetAxisRaw("Horizontal");
	inputVec.y = Input.GetAxisRaw("Vertical");

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

	Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
	rigid.MovePosition(rigid.position + nextVec);
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
	thePlayerAnimation.Dash();
  }

  IEnumerator DashCoroutine()
  {
	StartCoroutine(SpawnDashSprite());

	isDashing = true;
	//spriteRenderer.color = Color.cyan;

	rigid.AddForce(inputVec * dashSpeed, ForceMode2D.Impulse);

	yield return new WaitForSeconds(0.13f);
	rigid.velocity = Vector2.zero;

	yield return new WaitForSeconds(0.1f);
	//spriteRenderer.color = Color.white;

	thePlayerAnimation.Idle();
	isDashing = false;
  }

  IEnumerator SpawnDashSprite()
  {
	int count = 0;

	for (int i = 0; i < 15; i++)
	{
	  yield return new WaitForSeconds(0.01f);

	  GameObject dummy = Instantiate(dashDummy, transform.position, Quaternion.identity);

	  switch (count) 
	  {
		case 0:
		  dummy.GetComponent<SpriteRenderer>().color = new Color32(120, 246, 255, 255);
		  break;
		case 1:
		  dummy.GetComponent<SpriteRenderer>().color = new Color32(120, 246, 255, 255);
		  break;
		case 2:
		  dummy.GetComponent<SpriteRenderer>().color = new Color32(55, 159, 255, 255);
		  break;
		case 3:
		  dummy.GetComponent<SpriteRenderer>().color = new Color32(55, 159, 255, 255);
		  break;
		case 4:
		  dummy.GetComponent<SpriteRenderer>().color = new Color32(54, 86, 255, 255);
		  break;
		case 5:
		  dummy.GetComponent<SpriteRenderer>().color = new Color32(60, 54, 255, 255);
		  break;
		case 6:
		  dummy.GetComponent<SpriteRenderer>().color = new Color32(118, 54, 255, 255);
		  break;
		case 7:
		  dummy.GetComponent<SpriteRenderer>().color = new Color32(152, 54, 255, 255);
		  break;
		case 8:
		  dummy.GetComponent<SpriteRenderer>().color = new Color32(183, 54, 239, 255);
		  break;
	  }

	  count++;

	  if (count == 9)
		count = 8;
	}
  }

  private void SetAnim()
  {
	if (isDashing) return;

	if (inputVec.Equals(Vector2.zero))
	{
	  thePlayerAnimation.Idle();
	}
	else
	{
	  thePlayerAnimation.Walk();
	}
  }
}
