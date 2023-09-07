using System;
using UnityEngine;

namespace Managers
{
  public class ManagerLoader : MonoBehaviour
  {
    private void Awake()
    {
      if (FindObjectOfType<GameManager>() == null)
        Instantiate(Resources.Load("GameManager"));
      
      Destroy(gameObject);
    }
  }
}
