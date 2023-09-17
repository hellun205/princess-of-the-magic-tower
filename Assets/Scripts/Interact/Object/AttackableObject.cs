using System;
using UnityEngine;

namespace Interact.Object
{
  public class AttackableObject : Interacter
  {
    [Header("Attackable"), Min(1)]
    public int damage;
    
  }
}
