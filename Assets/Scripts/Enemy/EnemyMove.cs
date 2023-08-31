using Enemy;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] bool awake;
    [SerializeField] bool targetFind;

    [Range(0, 5)]
    [SerializeField] float rotateSpeed;
    [Range(0, 3)]
    [SerializeField] float dashSpeed;

    [SerializeField] float lookDistance;

    private Vector3 destination;

    private Transform targetObj;

    [Header("Dash")]
    [SerializeField] float maxCoolTime;
    private float currentCoolTime;

    [Header("Mask")]
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    private EnemyController enemyController;

    public bool AiAwake { get { return awake; } set { awake = value; } }

    public bool TargetFound { get { return targetFind; } set { targetFind = value; } }

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentCoolTime = maxCoolTime;

        targetObj = GameManager.Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!awake) return;

        FollowPlayer();
        Rotate();
    }

    private void FollowPlayer()
    {
        if (GameManager.Player == null)
        {
            print("Hanull");
            return;
        }

        //enemyController.rigidbody.velocity = new Vector2(transform.position.x + )

    }

    private void Rotate()
    {
        Quaternion newRotation = Quaternion.LookRotation(targetObj.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

        FindPlayer();
    }

    private void FindPlayer()
    {
            Debug.DrawRay(transform.position, destination);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, destination, lookDistance, targetMask);
        
            if (hit)
            {
                targetFind = true;
                Debug.Log(targetFind);

                StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        for(int i = 0; i< 30; i++)
        {
            yield return null;

            Vector3 targetVec = new Vector3(targetObj.position.x, targetObj.position.y, 0);
            transform.position = Vector3.Lerp(transform.position, targetVec, dashSpeed);
        }
    }
}
