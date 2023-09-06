using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dialogue
{
  public class DialoguePanel :MonoBehaviour, IPointerClickHandler
  {
    public void OnPointerClick(PointerEventData eventData)
    {
      GameManager.Dialogue.OnClick();
    }
  }
}
