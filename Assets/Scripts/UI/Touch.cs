using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UI
{
  public class Touch : MonoBehaviour, IPointerClickHandler
  {
    public UnityEvent<PointerEventData> onClick;
    
    public void OnPointerClick(PointerEventData eventData)
    {
      onClick?.Invoke(eventData);
    }
  }
}