using UnityEngine;

namespace Player
{
  public class DashDummy : MonoBehaviour
  {
    private void Start()
    {
      Destroy(gameObject, 0.2f);
    }
  }
}