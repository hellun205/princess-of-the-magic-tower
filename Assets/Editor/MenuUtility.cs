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
  }
}
