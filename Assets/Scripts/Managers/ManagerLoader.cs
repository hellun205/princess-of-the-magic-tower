using Map;
using Player;
using UnityEngine;

namespace Managers
{
  public class ManagerLoader : MonoBehaviour
  {
    public bool loadPlayer = true;
    public bool loadCamera = true;

    private void Awake()
    {
      if (loadPlayer && FindObjectOfType<PlayerManager>() == null)
        Instantiate(Resources.Load("Player"));
      
      if (loadCamera && FindObjectOfType<CameraController>() == null)
        Instantiate(Resources.Load("Camera"));
      
      if (FindObjectOfType<GameManager>() == null)
        Instantiate(Resources.Load("GameManager"));

      Destroy(gameObject);
    }
  }
}
