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

    public bool timeStopped;
    
    private void Start()
    {
        StartCoroutine(BugFix());
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        if ((currentPoint >= startPoint.Length) || !canMove)
        {
            return;
        }

        GoToDestination();
    }

    private void GoToDestination()
    {
        if ((transform.position - endPoint[currentPoint].position).magnitude <= 0.1f)
        {
            if(timeStopped) return;
            
            Debug.Log("a1");
            currentPoint++;
 
            if (currentPoint >= startPoint.Length) return;
            StartCoroutine(BoostCoroutine());
        }
        else
        {
            transform.DOMove(endPoint[currentPoint].position,
                (transform.position - endPoint[currentPoint].position).magnitude * (currentSpeed) * Time.deltaTime);
        }
    }

    private IEnumerator BoostCoroutine()
    {
        Debug.Log("a2");
        currentSpeed = boostSpeed;

        yield return new WaitUntil(() => (transform.position - startPoint[currentPoint].position).magnitude <= 1f);

        Debug.Log("a3");
        currentSpeed = moveSpeed;
    }

    private IEnumerator BugFix()
    {
        //int saveNumber = currentPoint;
        
        yield return new WaitUntil(() => Time.timeScale == 0);
        Debug.Log("timeStopped");
        timeStopped = true;

        yield return new WaitUntil(() => Time.timeScale != 0);

        yield return new WaitForSeconds(0.1f);

        //currentPoint = saveNumber;
        timeStopped = false;
        currentSpeed = moveSpeed;

        StartCoroutine(BugFix());
    }
}