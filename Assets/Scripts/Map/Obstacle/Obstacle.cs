using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
  private Animator animator;

  protected virtual void Awake()
  {
    animator = GetComponentInParent<Animator>();
  }

  protected virtual void OnBreak()
  {
    animator.SetTrigger("isBreak");
    Debug.Log($"{this.gameObject.name} Destroyed by Player");
    Destroy(this.gameObject, 0.17f); //0.17 = 공통 오브젝트 사라지는 애니메이션 재생시간
  }
}
