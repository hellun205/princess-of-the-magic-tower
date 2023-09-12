using UnityEditor;

namespace Editor
{
  public static class MenuUtility
  {
    [MenuItem("GameObject/Stage/Room")]
    public static void CreateRoom(MenuCommand menuCommand)
      => CreateUtility.CreatePrefab("Custom/Room/room",menuCommand);
    
    [MenuItem("GameObject/Stage/Door/Left")]
    public static void CreateDoorLeft(MenuCommand menuCommand) 
      => CreateUtility.CreatePrefab("Custom/Room/Door/left",menuCommand);
    
    [MenuItem("GameObject/Stage/Door/Right")]
    public static void CreateDoorRight(MenuCommand menuCommand) 
      => CreateUtility.CreatePrefab("Custom/Room/Door/right",menuCommand);
    
    [MenuItem("GameObject/Stage/Door/Top")]
    public static void CreateDoorTop(MenuCommand menuCommand) 
      => CreateUtility.CreatePrefab("Custom/Room/Door/top",menuCommand);
    
    [MenuItem("GameObject/Stage/Door/Bottom")]
    public static void CreateDoorBottom(MenuCommand menuCommand) 
      => CreateUtility.CreatePrefab("Custom/Room/Door/bottom",menuCommand);
    
    [MenuItem("GameObject/Stage/Link/Position")]
    public static void CreateLinkPosition(MenuCommand menuCommand) 
      => CreateUtility.CreatePrefab("Custom/Room/linkposition",menuCommand);
    
    [MenuItem("GameObject/Stage/Link/To")]
    public static void CreateLinkTo(MenuCommand menuCommand) 
      => CreateUtility.CreatePrefab("Custom/Room/linkto",menuCommand);    
    
    [MenuItem("GameObject/Stage/Summoner")]
    public static void CreateSummoner(MenuCommand menuCommand) 
      => CreateUtility.CreatePrefab("Custom/Room/summoner",menuCommand);
    
    [MenuItem("GameObject/Trap/Thorn")]
    public static void CreateTrapThorn(MenuCommand menuCommand) 
      => CreateUtility.CreatePrefab("Custom/Trap/thorn",menuCommand);
  }
}
