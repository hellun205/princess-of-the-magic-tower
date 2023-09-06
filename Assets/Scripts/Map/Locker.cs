using UnityEngine;
using System.Collections;

public class Locker : MonoBehaviour
{
  public string room;

  private BoxCollider2D col;

  private void Awake() {
    col = GetComponent<BoxCollider2D>();
  }

  public void UnLock() {
    col.enabled = false;
  }

  public void Lock() {
    col.enabled = true;
  }
}
