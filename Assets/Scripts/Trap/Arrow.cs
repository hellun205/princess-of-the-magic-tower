using Player;
using UnityEngine;

namespace Trap
{
  public class Arrow : MonoBehaviour
  {
    public float speed;

    private void Start()
    {
      Destroy(gameObject, 0.45f);
    }

    private void Update()
    {
      transform.Translate(Vector2.left * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.CompareTag("Player"))
      {
        collision.GetComponent<PlayerManager>().Death();
      }
    }
  }
}