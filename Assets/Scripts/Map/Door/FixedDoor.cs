namespace Map.Door
{
  public class FixedDoor : Door
  {
    public bool doorState;

    public override bool CheckClear => doorState;
  }
}