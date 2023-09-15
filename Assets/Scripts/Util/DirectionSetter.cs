using UnityEngine;

namespace Util
{
  public class DirectionSetter : MonoBehaviour
  {
    [SerializeField]
    private bool m_isEditMode;

    [SerializeField]
    private Vector2 m_targetPosition;

    public Vector2 value => m_targetPosition.normalized;

    private void OnDrawGizmosSelected()
    {
      var v1 = transform.position;
      var v2 = (Vector2) transform.position + (value * 1f);

      Gizmos.color = Color.cyan;
      Gizmos.DrawLine(v1, v2);

      if (m_isEditMode)
      {
        // Gizmos.DrawIcon(v2, "", false);
        Gizmos.DrawIcon((Vector2) transform.position + m_targetPosition, "", false, Color.red);
      }
        
    }
  }
}
