using System;

namespace Util
{
  public enum Direction
  {
    Left, Right, Bottom,
    Top
  }

  public static class DirectionUtility
  {
    public static Direction ToReverse(this Direction dir)
      => dir switch
      {
        Direction.Left   => Direction.Right,
        Direction.Right  => Direction.Left,
        Direction.Bottom => Direction.Top,
        Direction.Top    => Direction.Bottom,
        _                => throw new NotImplementedException()
      };

    public static char GetInitial(this Enum e) => e.ToString()[0];
  }
}
