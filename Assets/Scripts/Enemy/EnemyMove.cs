using System;
using Enemy;
using Managers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMove : MonoBehaviour
{
    [Header("Value")] public bool awake;
    public bool targetFind;

    [Range(0, 10)] public float rotateSpeed;
    [Range(0, 10)] public float dashSpeed;
    public float lookDistance;

    private Vector2 destination;

    public GameObject targetObj;

    [Header("Dash")] public bool canDash;

    public Transform dashPosition;

    public float maxCoolTime;
    private float currentCoolTime;

    [Header("Mask")] public LayerMask targetMask;
    public LayerMask obstacleMask;

    private EnemyController enemyController;
    private Rigidbody2D rigidbody;

    public bool AiAwake
    {
        get => awake;
        set => awake = value;
    }

    public bool TargetFound
    {
        get => targetFind;
        set => targetFind = value;
    }

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentCoolTime = maxCoolTime;

        targetObj = GameManager.Player.gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!awake) return;

        FindPlayer();
        DecreaseDashTime();
        FollowPlayer();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void DecreaseDashTime()
    {
        currentCoolTime -= Time.deltaTime;

        if (currentCoolTime <= 0)
        {
            currentCoolTime = maxCoolTime;
            canDash = true;
            targetFind = false;
        }
    }

    private void ResetCooltime()
    {
        currentCoolTime = maxCoolTime;
    }

    private void FollowPlayer()
    {
        if (GameManager.Player is null)
        {
            print("Hanull");
            return;
        }

        //enemyController.rigidbody.velocity = new Vector2(transform.position.x + )
    }

    private void Rotate()
    {
        if (targetFind) return;

        transform.Rotate(Vector3.forward * rotateSpeed);
    }

    private void FindPlayer()
    {
        if (!canDash)
        {
            Debug.DrawRay(transform.localPosition, transform.right * lookDistance, Color.red);
            return;
        }
        Debug.DrawRay(transform.localPosition, transform.right * lookDistance);
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, lookDistance, targetMask);

        if (hit)
        {
            targetFind = true;

            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        Vector3 targetVec = dashPosition.position;

        yield return new WaitForSeconds(0.3f);
        
        for (int i = 0; i <= 60; i++)
        {
            transform.position = Vector3.Lerp(transform.position, targetVec, dashSpeed);
            yield return null;
        }
        ResetCooltime();
    }
}