using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

namespace Ranking
{
  public class RankingScene : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI m_FloorText;

    public int currentFloor;

    public event Action<int> onFloorChanged;

    public int maxFloor;

    public Animator transitionAnimator;

    public void NextFloor()
    {
      if (currentFloor == maxFloor)
        ChangeFloor(0);
      else 
        ChangeFloor(currentFloor + 1);
    }

    public void PreviousFloor()
    {
      if (currentFloor == 0)
        ChangeFloor(maxFloor);
      else 
        ChangeFloor(currentFloor - 1);
    }

    public void ChangeFloor(int floor)
    {
      currentFloor = floor;
      m_FloorText.text = $"{currentFloor + 1}층";
      onFloorChanged?.Invoke(currentFloor);
    }

    public void GoToMain()
    {
      transitionAnimator.Play("Close");
      Utils.Wait(1f, () => SceneManager.LoadScene("TitleScene"));
    }

    // private void Awake()
    // {
    //   var data = new RankingData()
    //   {
    //     ranking = new []
    //     {
    //       new RankingData.Item
    //       {
    //         nickname = "Keke",
    //         profile = ProfileImage.Babel,
    //         record = 810.36f,
    //         death = 35
    //       },
    //       new RankingData.Item
    //       {
    //         nickname = "Tori",
    //         profile = ProfileImage.Chicken,
    //         record = 640.53f,
    //         death = 13
    //       },
    //       new RankingData.Item
    //       {
    //         nickname = "Bob",
    //         profile = ProfileImage.Slime,
    //         record = 785.13f,
    //         death = 21
    //       },
    //       new RankingData.Item
    //       {
    //         nickname = "Leon",
    //         profile = ProfileImage.Chicken,
    //         record = 512.89f,
    //         death = 17
    //       },
    //     }
    //   };
    //
    //   data.ranking = data.ranking.OrderBy(x => x.record).ThenBy(x => x.death).ToArray();
    //   
    //   PlayerPrefs.SetString("ranking", JsonUtility.ToJson(data));
    // }
  }
}
