using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
  Down, Up, Left, Right
}

public class LockedTurret : MonoBehaviour
{
  [Header("Value")] 
  public bool awake;
  public bool debugMode;
  
  public float fireCooltime;

  public float destroyTime;
  
  public Direction direction;
  public Vector2 dirVec;
  
  public Transform firePos;
  
  [Header("prefab")]
  public GameObject bullet_obj;

  private Animator animator;

  private void Awake()
  {
    animator = GetComponent<Animator>();
  }

  private void Start()
  {
    switch (direction)
    {
      case Direction.Left:
        dirVec = Vector2.left;
        break;
      case Direction.Right:
        dirVec = Vector2.right;
        break;
      case Direction.Up:
        dirVec = Vector2.up;
        break;
      case Direction.Down:
        dirVec = Vector2.down;
        break;
    }

    StartCoroutine(WaitTillAwake());
  }

  IEnumerator WaitTillAwake()
  {
    yield return new WaitUntil(() => awake);

    InvokeRepeating("Fire", 0f, fireCooltime);
  }
  
  private void Fire()
  {
    Bullet bullet = Instantiate(bullet_obj, firePos.position, Quaternion.identity).GetComponent<Bullet>();
    bullet.SetBulletAwake(dirVec, destroyTime);
    
    animator.SetTrigger("isFire");
  }
}