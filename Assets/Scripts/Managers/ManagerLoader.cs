using System;
using Player;
using UnityEngine;

namespace Managers
{
  public class ManagerLoader : MonoBehaviour
  {
    public bool loadPlayer = true;
    
    private void Awake()
    {
      if (loadPlayer && FindObjectOfType<PlayerManager>() == null)
        Instantiate(Resources.Load("Player"));
      
      if (FindObjectOfType<GameManager>() == null)
        Instantiate(Resources.Load("GameManager"));

      Destroy(gameObject);
    }
  }
}
