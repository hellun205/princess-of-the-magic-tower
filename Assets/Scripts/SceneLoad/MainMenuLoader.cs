using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

namespace SceneLoad
{
  public class MainMenuLoader : MonoBehaviour
  {
    public void OnStartButtonClick()
    {
      SceneManager.LoadScene("SampleScene 1");
    }

    public void OnOutButtonClick()
    {
      Utils.ExitGame();
    }
  }
}