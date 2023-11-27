using Managers;
using Scene;
using UnityEngine;
using Util;

namespace Interact.Object
{
  public class Boss1Trigger : InteractiveObject
  {
    protected override void OnInteract(GameObject caster)
    {
      Utils.Wait(1.5f, () =>
      {
        GameManager.Transition.Play(Transitions.FADEOUT, 2f);

        Utils.Wait(2.5f, () =>
        {
          GameManager.Map.MoveTo("room3");
          GameManager.Transition.Play(Transitions.IN);
        });
      });
    }
  }
}
