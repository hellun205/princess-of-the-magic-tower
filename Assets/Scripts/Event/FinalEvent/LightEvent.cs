using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class LightEvent : MonoBehaviour
{
  public Animator animator;

  public GameObject creditObj;
  
  private void Awake()
  {
    animator = GetComponent<Animator>();
  }

  public void StartEvent()
  {
    animator.SetTrigger("lightOn");
  }

  public void PlaySFX(string name)
  {
    GameManager.audioManager.PlaySfx(name);
  }

  public void CreditStart()
  {
    creditObj.SetActive(true);
  }
}