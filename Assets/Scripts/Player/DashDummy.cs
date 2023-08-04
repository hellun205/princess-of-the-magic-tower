using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDummy : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
	Invoke("Death", 0.2f);
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void Death()
  {
	Destroy(this.gameObject);
  }
}
