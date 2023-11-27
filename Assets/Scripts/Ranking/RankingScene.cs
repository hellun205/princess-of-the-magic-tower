using System;
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
  }
}
