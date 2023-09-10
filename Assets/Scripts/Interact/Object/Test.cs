using Managers;
using Scene;
using UnityEngine;

namespace Interact.Object
{
  public class Test : InteractiveObject
  {
    protected override void OnInteract(GameObject caster)
    {
      // GameManager.Scene.Load("Test", Transitions.FADEOUT, Transitions.FADEIN);
      new SceneLoader("Test")
       .Out(Transitions.FADEOUT)
       .In(Transitions.FADEIN)
       .Load();
    }
  }
}