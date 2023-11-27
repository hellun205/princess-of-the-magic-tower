using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class MoonArea : MonoBehaviour
{
  public GameObject hitEffect;
  public GameObject warningEffect;

  public bool isIn;
  
  public float currentCoolTime;
  public float maxCoolTime;

  private MoonMove moonMove;

  private void Awake()
  {
    moonMove = GetComponentInParent<MoonMove>();
  }

  // Start is called before the first frame update
  void Start()
  {
    currentCoolTime = maxCoolTime;
  }

  private void Update()
  {
    if (!moonMove.canMove) return;
    
    CoolTime();
  }

  private void CoolTime()
  {
    currentCoolTime -= Time.deltaTime;

    if (currentCoolTime <= 0)
    {
      if (isIn) return;

      StartCoroutine(SpawnAttackArea());
      currentCoolTime = maxCoolTime;
    }
  }

  private IEnumerator SpawnAttackArea()
  {
    warningEffect.transform.position = GameManager.Player.gameObject.transform.position;
    warningEffect.SetActive(true);

    yield return new WaitForSeconds(2f);
    
    warningEffect.SetActive(false);
  }
  
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.transform.CompareTag("Player"))
    {
      isIn = true;
      Debug.Log("In");
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.transform.CompareTag("Player"))
    {
      isIn = false;
      Debug.Log("Out");
    }
  }
}