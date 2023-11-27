using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ranking
{
  public class DeathRankingItem : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI m_deathCountText;
    
    [SerializeField]
    private Image m_profileImage;
    
    [SerializeField]
    private TextMeshProUGUI m_nicknameText;

    public void Set(int deathCount, ProfileImage profile, string nickname)
    {
      m_deathCountText.text = $"{deathCount}";
      m_profileImage.sprite = Resources.Load<Sprite>($"pi{(int)profile}");
      m_nicknameText.text = nickname;
    }
  }
}
