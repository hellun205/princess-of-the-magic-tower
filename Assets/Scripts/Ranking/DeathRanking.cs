using System.Linq;
using UnityEngine;

namespace Ranking
{
  public class DeathRanking : RankingList<DeathRankingItem>
  {
    protected override void Initalize()
    {
      if (!PlayerPrefs.HasKey("ranking")) return;

      var data = JsonUtility.FromJson<RankingData>(PlayerPrefs.GetString("ranking"));

      var list = data.ranking.OrderByDescending(x => x.death).ToArray();
      
      for (var i = 0; i < list.Length; i++)
      {
        var item = list[i];
        CreateItem(item.death, item.profile, item.nickname);
      }
    }

    private void CreateItem(int deathCount, ProfileImage profile, string nickname)
    {
      var obj = CreateObject();

      obj.Set(deathCount, profile, nickname);
    }
  }
}
