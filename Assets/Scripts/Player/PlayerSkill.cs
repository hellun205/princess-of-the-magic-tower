using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
  private PlayerMove thePlayerMove;
  public CanvasManager theCanvasManager;

  private int dashCount;
  private int maxDashCount;

  private float currentDashCooltime;
  public float maxDashCooltime;

  private void Awake()
  {
	thePlayerMove = GetComponent<PlayerMove>();
  }

  // Start is called before the first frame update
  void Start()
  {
	currentDashCooltime = maxDashCooltime;
  }

  // Update is called once per frame
  void Update()
  {
	if (Input.GetKeyDown(KeyCode.E))
	{
	  Dash();
	}

	currentDashCooltime -= Time.deltaTime;

	if (currentDashCooltime <= 0)
	{
	  currentDashCooltime = maxDashCooltime;

	  if (dashCount >= 3) return;

	  dashCount++;
	}

	theCanvasManager.SetDashTxt = dashCount + " / 3";
  }

  public void Dash()
  {
	if (dashCount <= 0) return;

	thePlayerMove.Dash();
	dashCount--;
  }

}
