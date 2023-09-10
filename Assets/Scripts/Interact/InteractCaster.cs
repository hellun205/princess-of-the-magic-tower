using System;

namespace Interact
{
  [Flags]
  public enum InteractCaster
  {
    Player =  1 << 1,
    Others = 1 << 2
  }
}
