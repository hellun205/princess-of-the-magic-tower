using System;
using Map.Door;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
  [CustomEditor(typeof(Door))]
  public class DoorEditor : UnityEditor.Editor
  {
    private Door _target;
    private SerializedProperty sp_type, sp_direction, sp_clear, sp_state, sp_ignoreX, sp_ignoreY;
    
    private void OnEnable()
    {
      _target = target as Door;
      sp_type = serializedObject.FindProperty("type");
      sp_direction = serializedObject.FindProperty("direction");
      sp_state = serializedObject.FindProperty("state");
      sp_clear = serializedObject.FindProperty("clearCount");
      sp_ignoreX = serializedObject.FindProperty("ignoreX");
      sp_ignoreY = serializedObject.FindProperty("ignoreY");

    }
    public override void OnInspectorGUI()
    {
      // base.OnInspectorGUI();
      serializedObject.Update();

      EditorGUILayout.PropertyField(sp_direction);
      EditorGUILayout.PropertyField(sp_type);

      switch (sp_type.enumValueIndex)
      {
        case 0:
          
          EditorGUILayout.PropertyField(sp_state);
          break;
          case 1:
            EditorGUILayout.PropertyField(sp_clear);
          break;
      }
      
      
      EditorGUILayout.PropertyField(sp_ignoreX);
      EditorGUILayout.PropertyField(sp_ignoreY);
      
      EditorUtility.SetDirty(_target);
      serializedObject.ApplyModifiedProperties();
    }
  }
}
