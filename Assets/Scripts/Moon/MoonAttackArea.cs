using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonAttackArea : MonoBehaviour
{
  private MoonAttack moonAttack;

  // Start is called before the first frame update
  void Start()
  {
    moonAttack = FindObjectOfType<MoonAttack>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.transform.CompareTag("Player"))
      moonAttack.isIn = true;
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.transform.CompareTag("Player"))
      moonAttack.isIn = false;
  }
}