using System.Collections;
using System.Collections.Generic;
using Managers;
using Map;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    public void CreditEnd()
    {
      GameManager.Manager.Clear();
      Destroy(FindObjectOfType<GameManager>().gameObject);
      Destroy(FindObjectOfType<PlayerManager>().gameObject);
      Destroy(FindObjectOfType<CameraController>().gameObject);
      SceneManager.LoadScene("Clear");
    }
}
