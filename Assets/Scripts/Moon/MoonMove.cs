using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoonMove : MonoBehaviour
{
  public bool canMove;
  
  public Transform[] startPoint;
  public Transform[] endPoint;
  public int currentPoint;

  public float currentSpeed;
  
  public float moveSpeed;
  public float boostSpeed;

  // Update is called once per frame
  void Update()
  {
    if ((currentPoint >= startPoint.Length) || !canMove)
    {
      return;
    }
    GoToDestination();
  }
  
  private void GoToDestination()
  {
    if ((transform.position - endPoint[currentPoint].position).magnitude <= 1f)
    {
      Debug.Log("arrive");
      currentPoint++;

      if (currentPoint >= startPoint.Length) return;
      StartCoroutine(BoostCoroutine());
    }
    else
    {
      transform.DOMove(endPoint[currentPoint].position, (transform.position - endPoint[currentPoint].position).magnitude * (currentSpeed) * Time.deltaTime); 
      Debug.Log("Moving");
    }
  }

  private IEnumerator BoostCoroutine()
  {
    currentSpeed = boostSpeed;
    
    yield return new WaitUntil(() => (transform.position - startPoint[currentPoint].position).magnitude <= 1f);
    
    currentSpeed = moveSpeed;
  }
}