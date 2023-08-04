using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpear : MonoBehaviour
{
  public GameObject Trap_obj;

  PlayerManager thePlayerManager;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
	if (collision.CompareTag("Player"))
	{
	  Trap_obj.SetActive(true);

	  thePlayerManager = collision.GetComponent<PlayerManager>();

	  Invoke("Dead", 0.8f);
	}
  }

  private void Dead()
  {
	thePlayerManager.Death();
  }
}
