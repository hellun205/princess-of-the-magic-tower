using System;

namespace Player.UI
{
  [Flags]
  public enum DashType : int
  {
    None = 0,
    Normal = 1 << 1,
    Additional = 1 << 2,
    All = int.MaxValue
  }
}
