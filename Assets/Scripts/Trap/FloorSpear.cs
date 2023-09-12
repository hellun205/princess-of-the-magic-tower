using Player;
using UnityEngine;

namespace Trap
{
  public class FloorSpear : MonoBehaviour
  {
    public GameObject Trap_obj;

    PlayerManager thePlayerManager;

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
}