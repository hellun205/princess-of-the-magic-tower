using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game
{
  public class CanvasManager : MonoBehaviour
  {
    [SerializeField]
    TextMeshProUGUI dash_txt;

    public GameObject deathImage;

    // Start is called before the first frame update
    void Start()
    {
    }

    public string SetDashTxt
    {
      set { dash_txt.text = value; }
    }

    public void Death()
    {
      StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
      deathImage.SetActive(true);

      yield return new WaitForSeconds(2f);

      deathImage.SetActive(false);
    }
  }
}
