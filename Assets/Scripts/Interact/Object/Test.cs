using Managers;
using Scene;

namespace Interact.Object
{
  public class Test : InteractiveObject
  {
    protected override void OnInteract()
    {
      // GameManager.Scene.Load("Test", Transitions.FADEOUT, Transitions.FADEIN);
      new SceneLoader("Test")
       .Out(Transitions.FADEOUT)
       .In(Transitions.FADEIN)
       .Load();
    }
  }
}