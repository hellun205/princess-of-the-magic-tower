using System;
using System.Collections;
using Managers;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
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

    public Animator nicknamePanelAnimator;

    public Animator transitionPanelAnimator;

    public TMP_InputField nicknameInputField;

    public Button button;

    public void OnStartButtonClick()
    {
      nicknamePanelAnimator.Play("open");
    }

    public void RealStartButtonClick()
    {
      transitionPanelAnimator.Play("Out");
      GameManager.nickname = nicknameInputField.text;
      Utils.Wait(1, () => SceneManager.LoadScene("Scenes/Stage/test"));
    }

    public void OnLoadButtonClick()
    {
      var data = GameManager.LoadData();
      GameManager.InitLoad(true);
      SceneManager.LoadScene(data.stage);
    }

    public void OnRankingButtonClick()
    {
      transitionPanelAnimator.Play("Out");
      Utils.Wait(1f, () => SceneManager.LoadScene("Ranking"));
    }

    public void OnOutButtonClick()
    {
      Utils.ExitGame();
    }

    private void Awake()
    {
      foreach (var mainMenuPage in pageData)
        mainMenuPage.ui.SetVisible(false);

      button.SetVisible(false);
      nicknameInputField.onValueChanged.AddListener(value =>
      {
        if (string.IsNullOrEmpty(value.Trim()))
          button.SetVisible(false);
        else if (!button.IsVisible())
          button.SetVisible(true, 0.2f);
          
      });
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
