using System;
using Managers;
using Ranking;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

namespace SceneLoad
{
  public class ClearScene : MonoBehaviour
  {
    public ClearRankingItem rank1;
    public ClearRankingItem rank2;
    public ClearRankingItem rank3;

    public TextMeshProUGUI record;

    private void Awake()
    {
      record.text = $"죽은 횟수 : {GameManager.death:d2}\n플레이 타임 : {GameManager.record.RecordToString()}";
      
      if (!PlayerPrefs.HasKey("ranking"))
      {
        rank1.gameObject.SetActive(false);
        rank2.gameObject.SetActive(false);
        rank3.gameObject.SetActive(false);
        return;
      }

      var data = JsonUtility.FromJson<RankingData>(PlayerPrefs.GetString("ranking"));

      rank1.gameObject.SetActive(data.ranking.Length >= 1);
      rank2.gameObject.SetActive(data.ranking.Length >= 2);
      rank3.gameObject.SetActive(data.ranking.Length >= 3);
      
      if (rank1.gameObject.activeSelf)
      {
        var item = data.ranking[0];
        rank1.Set(item.profile, item.nickname, item.record);
      }
      
      if (rank2.gameObject.activeSelf)
      {
        var item = data.ranking[1];
        rank2.Set(item.profile, item.nickname, item.record);
      }
      
      if (rank3.gameObject.activeSelf)
      {
        var item = data.ranking[2];
        rank3.Set(item.profile, item.nickname, item.record);
      }
    }

    public void GotoMain()
    {
      SceneManager.LoadScene("TitleScene");
    }
  }
}
