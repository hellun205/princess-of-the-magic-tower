using System;

namespace Trap
{
  [Flags]
  public enum TrapCondition
  {
    Detect = 1 << 1,
    Repeat = 1 << 2,
  }
}
