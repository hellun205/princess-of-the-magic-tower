using Managers;
using Map;
using UnityEngine;

namespace Interact.Object
{
  public class SavePoint : InteractiveObject, IRequireRoom
  {
    public Room room { get; set; }
    
    protected override void OnInteract(GameObject caster)
    {
      if (!room.isCleared) return;
      
      GameManager.Manager.Save(room.name);
    }
  }
}
