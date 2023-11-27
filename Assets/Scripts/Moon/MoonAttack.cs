using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class MoonAttack : MonoBehaviour
{
  public ParticleSystem attackTrail;

  public bool isIn;
  
  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
  }

  public void PlayParticle()
  {
    attackTrail.Play();
    Debug.Log("ParticlePlay");
  }
  
  public void AttackPlayer() 
  {
    if (isIn)
    {
      GameManager.Player.Death();
    }
  }
}