using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Ranking
{
  public class ClearRankingItem : MonoBehaviour
  {
    [SerializeField]
    private Image m_profileImage;

    [SerializeField]
    private TextMeshProUGUI m_text;

    public void Set(ProfileImage profile, string nickname, float record)
    {
      m_profileImage.sprite = Resources.Load<Sprite>($"pi{(int)profile}");
      m_text.text = $"{nickname}\n{record.RecordToString()}";
    }
  }
}
