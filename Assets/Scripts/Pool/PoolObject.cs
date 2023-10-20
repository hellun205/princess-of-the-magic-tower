using Managers;
using UnityEngine;

namespace Pool
{
  public class PoolObject : MonoBehaviour
  {
    [Header("Pool Object")]
    public int index;

    public string type;

    public virtual Vector2 position
    {
      get => transform.position;
      set => transform.position = value;
    }

    public delegate void PoolObjectEventListener();

    public event PoolObjectEventListener onGet;
    public event PoolObjectEventListener onReleased;

    public void OnGet()
    {
        onGet?.Invoke();
    }

    public void OnReleased()
    {
        onReleased?.Invoke();
    }

    public void Release() => GameManager.Pool.pools[type].Release(this);
  }
}