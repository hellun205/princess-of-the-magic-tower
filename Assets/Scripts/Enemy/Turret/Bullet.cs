using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Player;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Bullet : MonoBehaviour
{
  public bool awake;
  
  public float speed;
  
  private float lifeTime;
  private Vector2 direction;
  
  private void Start()
  {
    StartCoroutine(FireCoroutine());
  }

  private void Update()
  {
    if (!awake) return;
    
    transform.Translate((direction * speed) * Time.deltaTime);
  }

  public void SetBulletAwake(Vector2 direction, float lifeTime)
  {
    this.direction = direction;
    this.lifeTime = lifeTime;

    awake = true;
  }

  IEnumerator FireCoroutine()
  {
    yield return new WaitUntil(() => awake);
    
    Destroy(this.gameObject, lifeTime);
  }
  
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      collision.GetComponent<PlayerManager>().Death();
    }
  }
}