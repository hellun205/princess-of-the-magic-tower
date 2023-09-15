using System;
using System.Collections.Generic;
using System.Linq;
using Map;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
  [CustomEditor(typeof(StageController))]
  public class StageContollerEditor : UnityEditor.Editor
  {
    private StageController _target;
    private List<string> roomNames;
    private SerializedProperty sp_startRoom;

    private void OnEnable()
    {
      _target = target as StageController;
      sp_startRoom = serializedObject.FindProperty("startRoom");

      if (_target != null)
        roomNames = _target.GetComponentsInChildren<Room>().Select(room => room.name).ToList();
    }

    public override void OnInspectorGUI()
    {
      var curIndex = roomNames.IndexOf(sp_startRoom.stringValue);
      var newIndex = EditorGUILayout.Popup(new GUIContent("Start Room"), curIndex, roomNames.ToArray());
      
      if (newIndex != curIndex)
      {
        serializedObject.Update();
        sp_startRoom.stringValue = roomNames[newIndex];
        EditorUtility.SetDirty(_target);
        serializedObject.ApplyModifiedProperties();
      }
    }
  }
}
