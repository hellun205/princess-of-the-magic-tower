using System;
using Managers;
using Map;
using UnityEngine;

namespace Interact.Object
{
  public class SavePoint : InteractiveObject, IRequireRoom
  {
    public Room room { get; set; }

    public Animator anim;
    
    private void Awake()
    {
      anim = GetComponent<Animator>();
    }
    
    protected override void OnInteract(GameObject caster)
    {
      // if (!room.isCleared) return;
      
      GameManager.Manager.Save(room.name, name);
      anim.Play("Open");
    }
  }
}
