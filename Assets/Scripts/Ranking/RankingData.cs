using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Ranking
{
  [Serializable]
  public class RankingData
  {
    [Serializable]
    public class Item
    {
      public string nickname;
      public ProfileImage profile;
      public float record;
      public int death;
    }

    public Item[] ranking;
  }
}
