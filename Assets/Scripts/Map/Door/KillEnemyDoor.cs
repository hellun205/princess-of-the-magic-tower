namespace Map.Door
{
  public class KillEnemyDoor : Door
  {
    public override bool CheckClear => room.enemies.Count == 0;
  }
}
