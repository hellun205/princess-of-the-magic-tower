using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public CanvasManager theCanvasManager;

  public Transform spawnPosition;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Death()
  {
	theCanvasManager.Death();
	transform.position = spawnPosition.position;
  }
}
