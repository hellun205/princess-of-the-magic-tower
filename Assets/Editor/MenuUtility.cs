using UnityEditor;
using UnityEngine;

namespace Editor
{
  public static class MenuUtility
  {
    [MenuItem("GameObject/Custom/Room")]
    public static void CreateRoom(MenuCommand menuCommand)
    {
      CreateUtility.CreateObject("test",menuCommand, typeof(CircleCollider2D));
    }
    
    [MenuItem("GameObject/Custom/Door/Left")]
    public static void CreateDoorLeft(MenuCommand menuCommand) 
      => CreateUtility.CreatePrefab("Custom/Room/Door/left",menuCommand);
    
    [MenuItem("GameObject/Custom/Door/Right")]
    public static void CreateDoorRight(MenuCommand menuCommand) 
      => CreateUtility.CreatePrefab("Custom/Room/Door/right",menuCommand);
    
    [MenuItem("GameObject/Custom/Door/Top")]
    public static void CreateDoorTop(MenuCommand menuCommand) 
      => CreateUtility.CreatePrefab("Custom/Room/Door/top",menuCommand);
    
    [MenuItem("GameObject/Custom/Door/Bottom")]
    public static void CreateDoorBottom(MenuCommand menuCommand) 
      => CreateUtility.CreatePrefab("Custom/Room/Door/bottom",menuCommand);
  }
}
