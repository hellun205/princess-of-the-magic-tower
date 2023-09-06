using Managers;
using Scene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoad
{
  public class MainMenuLoader : MonoBehaviour
  {
    public void OnStartButtonClick()
    {
      // GameManager.Scene.Load("SampleScene", Transitions.FADEOUT, Transitions.FADEIN);
      SceneManager.LoadScene("SampleScene");
    }
  }
}