using System;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using Util;

namespace SceneLoad
{
  public class MainMenuLoader : MonoBehaviour
  {
    [Serializable]
    public struct MainMenuPage
    {
      public bool isPlayed;
      public float animationDuration;
      public VideoPlayer player;
      public GameObject ui;
    }

    public SlidePage slidePage;

    public MainMenuPage[] pageData;

    public void OnStartButtonClick()
    {
      SceneManager.LoadScene("Scenes/Stage/test");
    }

    public void OnOutButtonClick()
    {
      Utils.ExitGame();
    }

    private void Awake()
    {
      foreach (var mainMenuPage in pageData)
        mainMenuPage.ui.SetVisible(false);
    }

    private void Start()
    {
      foreach (var mainMenuPage in pageData)
        mainMenuPage.player.targetTexture.Release();

      SetSlideIndex(0);
    }

    public void SetSlideIndex(int index)
    {
      slidePage.index = index;

      var page = pageData[index];
      if (!page.isPlayed)
      {
        pageData[index].isPlayed = true;
        Utils.Wait(0.5f, () =>
        {
          page.player.Play();

          Utils.Wait(page.animationDuration, () => page.ui.SetVisible(true, 0.1f));
        });
      }
    }
  }
}
