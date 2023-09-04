using UnityEngine;
using System.Collections;
using Scene;
using Managers;

public class Test : InteractiveObject
{
  protected override void OnInteract() {
    GameManager.Scene.Load("Test", new TransitionOption(Transitions.FADEOUT), new TransitionOption(Transitions.FADEIN));
  }

}
