using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
  private LightEvent lightEvent;

  private void Awake()
  {
    lightEvent = GetComponentInChildren<LightEvent>();
  }


  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.transform.CompareTag("Player"))
    {
      GameManager.Player.light.SetLight("all");
      GameManager.Player.move.canMove = false;
      lightEvent.StartEvent();
    }
  }
}