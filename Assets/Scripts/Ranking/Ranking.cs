using UnityEngine;

namespace Ranking
{
  public class Ranking : RankingList<RankingItem>
  {
    [SerializeField]
    private RankingScene rankingScene;

    protected override void Awake()
    {
      base.Awake();
      rankingScene.onFloorChanged += RankingSceneOnonFloorChanged;
    }

    private void RankingSceneOnonFloorChanged(int floor)
    {
      Clear();
      if (floor == 0)
      {
        Initalize();
      }
    }

    protected override void Initalize()
    {
      if (!PlayerPrefs.HasKey("ranking")) return;

      var data = JsonUtility.FromJson<RankingData>(PlayerPrefs.GetString("ranking"));

      for (var i = 0; i < data.ranking.Length; i++)
      {
        var item = data.ranking[i];
        CreateItem(i, item.profile, item.nickname, item.record, item.death);
      }
    }

    private void CreateItem(int tier, ProfileImage? profile, string nickname, float record, int deathCount)
    {
      var obj = CreateObject();

      obj.Set(tier, profile, nickname, record, deathCount);
    }
  }
}
