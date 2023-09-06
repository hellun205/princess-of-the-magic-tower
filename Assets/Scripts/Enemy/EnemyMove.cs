using Enemy.AI;
using UnityEngine;

namespace Enemy
{
  public class EnemyMove : MonoBehaviour
  {
    private EnemyAI ai;

    private void Awake()
    {
      ai = GetComponent<EnemyAI>();
    }

    public void StartAI()
    {
      if (ai != null) ai.StartAI();
    }
  }
}