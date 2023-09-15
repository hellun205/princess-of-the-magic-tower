namespace Map.Door
{
  public static class DoorUtility
  {
    public static bool GetCondition(this Door door)
    {
      return door.type switch
      {
        DoorType.Fixed => door.state,
        DoorType.EnemyCleared => door.room.enemies.Count == door.clearCount,
        _ => false,
      };
    }
  }
}
