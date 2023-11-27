using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Ranking
{
  public class RankingItem : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI m_tierText;

    [SerializeField]
    private Image m_profileImg;

    [SerializeField]
    private TextMeshProUGUI m_nicknameText;

    [SerializeField]
    private TextMeshProUGUI m_recordText;

    public void Set(int tier, ProfileImage? profile, string nickname, float record, int deathCount)
    {
      m_tierText.text = tier switch
      {
        0 or 1 or 2 => $"<sprite={tier}>",
        _           => $"{tier + 1}"
      };
      m_profileImg.sprite = null; // Todo
      m_nicknameText.text = nickname;
      
      m_recordText.text = $"기록 : {record.RecordToString()}\n죽은 횟수 : {deathCount:d2}";
    }
  }
}
