using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
  public float speed;

  // Start is called before the first frame update
  void Start()
  {
	Destroy(this.gameObject, 0.45f);
  }

  // Update is called once per frame
  void Update()
  {
	transform.Translate(Vector2.left * speed);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
	if(collision.CompareTag("Player"))
	{
	  collision.GetComponent<PlayerManager>().Death();
	}
  }
}
