using Managers;
using Scene;
using UnityEngine;
using Util;

namespace Interact.Object
{
  public class LightSwitch : InteractiveObject
  {
    [Header("Light Switch")]
    public bool turnToNight;

    public string moveTo;

    protected override void OnInteract(GameObject caster)
    {
      Utils.Wait(0.5f, () =>
      {
        Utils.Pause();
        GameManager.Transition.Play(Transitions.OUT);
        var pos = GameManager.PlayerLocation.GetPositionInRoom();
        GameManager.Map.MoveTo(moveTo);
        GameManager.PlayerLocation.SetPositionInRoom(pos);
        
        Utils.UnPause();
        Utils.Wait(1f, () =>
        {
          GameManager.Player.light.SetLight(turnToNight ? "night" : "default");
          GameManager.Transition.Play(Transitions.IN);
        });
      });

    }
  }
}