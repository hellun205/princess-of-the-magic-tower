using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class LightSwitch : MonoBehaviour
{
  public bool isNightSwitch = false;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.transform.CompareTag("Player"))
    {
      if (isNightSwitch)
        GameManager.Player.SwitchToNight(true);
      else
        GameManager.Player.SwitchToNight(false);
    }
  }
}
