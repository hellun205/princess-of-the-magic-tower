using UnityEngine;
using System.Collections;

public abstract class InteractiveObject : MonoBehaviour
{

  protected abstract void OnInteract();

  // Use this for initialization
  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag("Player")) {
      OnInteract();
    }
  }
}
