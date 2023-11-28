using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
  
  
    public void CreditEnd()
    {
      GameManager.Manager.Clear();
      SceneManager.LoadScene("Clear");
    }
}
