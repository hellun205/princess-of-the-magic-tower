using System;

namespace Interact
{
  [Flags]
  public enum InteractCondition
  {
    Walk = 1 << 1,
    Dash = 1 << 2,
  }
}
