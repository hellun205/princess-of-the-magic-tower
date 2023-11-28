using Managers;
using Scene;
using UnityEngine;

namespace Interact.Object
{
  public class Clear : InteractiveObject
  {
    protected override void OnInteract(GameObject caster)
    {
      GameManager.Map.LoadStageFromSceneName("2", Transitions.FADEOUT, Transitions.FADEIN, () =>
      {
        GameManager.Player.light.SetDefault();
      });
    }
  }
}
