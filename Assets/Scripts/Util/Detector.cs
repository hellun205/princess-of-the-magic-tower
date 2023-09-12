using UnityEngine;

namespace Util
{
  [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
  public class Detector : MonoBehaviour
  {
    public delegate void DetectorEventListener(GameObject detectedObject);

    public event DetectorEventListener onEnter;
    public event DetectorEventListener onStay;
    public event DetectorEventListener onExit;

    public LayerMask targetLayer;

    private Collider2D col;
    private Rigidbody2D rigid;

#if UNITY_EDITOR
    private void Reset()
    {
      var c = GetComponent<Collider2D>();
      var r = GetComponent<Rigidbody2D>();
      c.isTrigger = true;
      r.constraints = RigidbodyConstraints2D.FreezeAll;
      r.gravityScale = 0;
    }
#endif

    private void Awake()
    {
      col = GetComponent<Collider2D>();
      rigid = GetComponent<Rigidbody2D>();
      SetTarget(targetLayer);
    }

    public void SetTarget(LayerMask layerMask)
    {
      if (rigid == null) rigid = GetComponent<Rigidbody2D>();

      targetLayer = layerMask;
      rigid.includeLayers = targetLayer;
      rigid.excludeLayers = ~ targetLayer;
    }

    private void OnTriggerEnter2D(Collider2D other) => onEnter?.Invoke(other.gameObject);

    private void OnTriggerStay2D(Collider2D other) => onStay?.Invoke(other.gameObject);

    private void OnTriggerExit2D(Collider2D other) => onExit?.Invoke(other.gameObject);
  }
}
