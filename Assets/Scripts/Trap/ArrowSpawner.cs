using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
  [SerializeField] private GameObject arrow_obj;
  [SerializeField] private float coolTime;

  // Start is called before the first frame update
  void Start()
  {
	InvokeRepeating("SpawnArrow", 0f, coolTime);
  }

  private void SpawnArrow()
  {
	Instantiate(arrow_obj, transform.position, Quaternion.identity);
  }
}
