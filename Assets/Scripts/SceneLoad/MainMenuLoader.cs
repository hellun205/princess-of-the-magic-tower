using Managers;
using Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

namespace SceneLoad
{
  public class MainMenuLoader : MonoBehaviour
  {
    public void OnStartButtonClick()
    {
      // GameManager.Scene.Load("SampleScene", Transitions.FADEOUT, Transitions.FADEIN);
      SceneManager.LoadScene("SampleScene 1");
    }

    public void OnOutButtonClick()
    {
      Utils.ExitGame();
    }
  }
}