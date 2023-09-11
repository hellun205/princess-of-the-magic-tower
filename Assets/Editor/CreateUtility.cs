using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
  public static class CreateUtility
  {
    public static void CreatePrefab(string path, MenuCommand menuCommand)
    {
      var o = PrefabUtility.InstantiatePrefab(Resources.Load(path)) as GameObject;
      Place(o, menuCommand);
    }

    public static void CreateObject(string name, MenuCommand menuCommand, params Type[] types)
    {
      var o = ObjectFactory.CreateGameObject(name, types);
      Place(o, menuCommand);
    }

    public static void Place(GameObject gameObject, MenuCommand menuCommand = null)
    {
      EditorApplication.update += EngageRenameMode;
      renameTime = EditorApplication.timeSinceStartup + 0.15d;
      var lastView = SceneView.lastActiveSceneView;
      gameObject.transform.position = lastView ? lastView.pivot : Vector3.zero;

      StageUtility.PlaceGameObjectInCurrentStage(gameObject);
      // GameObjectUtility.EnsureUniqueNameForSibling(gameObject);

      if (menuCommand != null)
        GameObjectUtility.SetParentAndAlign(gameObject, menuCommand.context as GameObject);

      Undo.RegisterCreatedObjectUndo(gameObject, $"Create Object: {gameObject.name}");
      Selection.activeGameObject = gameObject;

      EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    private static double renameTime;
    private static void EngageRenameMode()
    {
      if (EditorApplication.timeSinceStartup >= renameTime)
      {
        EditorApplication.update -= EngageRenameMode;
        var type = typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
        var hierarchyWindow = EditorWindow.GetWindow(type);
        hierarchyWindow.SendEvent(new Event() { type = EventType.KeyDown, keyCode = KeyCode.F2 });
      }
    }
  }
}
