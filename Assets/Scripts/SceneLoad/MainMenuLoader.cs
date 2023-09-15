using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

namespace SceneLoad
{
  public class MainMenuLoader : MonoBehaviour
  {
    public void OnStartButtonClick()
    {
      SceneManager.LoadScene("Scenes/Stage/test");
    }

    public void OnOutButtonClick()
    {
      Utils.ExitGame();
    }
  }
}