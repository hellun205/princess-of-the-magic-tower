using Managers;
using Scene;

namespace Interact
{
  public class Test : InteractiveObject
  {
    protected override void OnInteract()
    {
      GameManager.Scene.Load("Test", Transitions.FADEOUT, Transitions.FADEIN);
    }
  }
}