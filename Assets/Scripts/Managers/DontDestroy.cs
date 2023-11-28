using System;
using UnityEngine;

namespace Managers
{
  public class DontDestroy : MonoBehaviour
  {
    public static bool isExist;
    
    private void Awake()
    {
      if (isExist)
      {
        Destroy(gameObject);
        return;
      }
      DontDestroyOnLoad(gameObject);
      isExist = true;
    }
  }
}