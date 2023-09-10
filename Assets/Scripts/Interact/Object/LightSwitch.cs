using Managers;
using UnityEngine;

namespace Interact.Object
{
  public class LightSwitch : InteractiveObject
  {
    [Header("Light Switch")]
    public bool turnToNight;
    
    protected override void OnInteract(GameObject caster)
    {
      GameManager.Player.light.SetLight(turnToNight ? "night" : "default");
    }
  }
}
