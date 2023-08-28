using Managers;
using TMPro;
using UnityEngine;

namespace Player
{
  public class PlayerSkill : MonoBehaviour
  {
    private TextMeshProUGUI dashTxt;
    private PlayerMove thePlayerMove;

    [SerializeField]
    private int dashCount;
    
    [SerializeField]
    private int maxDashCount = 3;

    private float currentDashCooltime;
    public float maxDashCooltime;

    private void Awake()
    {
      thePlayerMove = GetComponent<PlayerMove>();
      dashTxt = GameManager.ManagedObject.Find<TextMeshProUGUI>("$dash_count");
    }

    // Start is called before the first frame update
    void Start()
    {
      currentDashCooltime = maxDashCooltime;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.LeftShift))
      {
        Dash();
      }

      currentDashCooltime -= Time.deltaTime;

      if (currentDashCooltime <= 0)
      {
        currentDashCooltime = maxDashCooltime;

        if (dashCount >= maxDashCount) return;

        dashCount++;
      }

      dashTxt.text = $"{dashCount} / {maxDashCount}";
    }

    public void Dash()
    {
      if (dashCount <= 0) return;

      thePlayerMove.Dash();
      dashCount--;
    }
  }
}