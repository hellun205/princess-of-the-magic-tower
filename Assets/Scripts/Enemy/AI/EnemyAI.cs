using UnityEngine;

namespace Enemy.AI
{
  public abstract class EnemyAI : MonoBehaviour
  {
    public bool isAttacking;

    public abstract void StartAI();
    
    
  }
}