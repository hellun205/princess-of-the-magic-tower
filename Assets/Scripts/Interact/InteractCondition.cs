using System;

namespace Interact
{
  [Flags]
  public enum InteractCondition
  {
    Reach = 1 << 1,
    Attack = 1 << 2,
  }
}
