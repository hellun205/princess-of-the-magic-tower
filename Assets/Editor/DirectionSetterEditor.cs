using System;
using UnityEditor;
using UnityEngine;
using Util;

namespace Editor
{
  [CustomEditor(typeof(DirectionSetter))]
  public class DirectionSetterEditor : UnityEditor.Editor
  {
    private DirectionSetter _target;
    private SerializedProperty sp_targetPosition, sp_isEditMode;
    private bool isEditMode;
    private bool isClick;
    private bool isShift;

    private void OnEnable()
    {
      _target = target as DirectionSetter;
      sp_targetPosition = serializedObject.FindProperty("m_targetPosition");
      sp_isEditMode = serializedObject.FindProperty("m_isEditMode");
    }

    private void OnDisable()
    {
      isEditMode = false;
      serializedObject.Update();
      sp_isEditMode.boolValue = false;
      EditorUtility.SetDirty(_target);
      serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
      // base.OnInspectorGUI();
      serializedObject.Update();

      EditorGUILayout.PropertyField(sp_targetPosition);
      var normalized = sp_targetPosition.vector2Value.normalized;
      EditorGUILayout.LabelField($"normalized value: ({normalized.x}, {normalized.y})");

      isEditMode = GUILayout.Toggle(isEditMode, "Edit direction", EditorStyles.miniButton);
      sp_isEditMode.boolValue = isEditMode;

      EditorUtility.SetDirty(_target);
      serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
      if (!isEditMode) return;
      var e = Event.current;
      var mousePos = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;

      if (e.type == EventType.MouseDown && e.button == 0)
      {
        UseEvent();

        // var pos = (Vector2) _target.transform.position + sp_targetPosition.vector2Value.normalized;
        // if ((pos - (Vector2) mousePos).magnitude <= 0.2f)
        isClick = true;
      }
      else if (e.type == EventType.MouseUp && e.button == 0)
        isClick = false;

      if (e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftShift)
      {
        UseEvent();
        isShift = true;
      }
      else if (e.type == EventType.KeyUp && e.keyCode == KeyCode.LeftShift)
      {
        UseEvent();
        isShift = false;
      }

      if (e.type == EventType.MouseDrag && e.button == 0 && isClick)
      {
        UseEvent();
        serializedObject.Update();

        var pos = mousePos - _target.transform.position;

        if (isShift && pos.x is < 0.3f and > -0.3f)
          pos.x = 0f;
        else if (isShift && pos.y is < 0.3f and > -0.3f)
          pos.y = 0f;

        sp_targetPosition.vector2Value = pos;
        serializedObject.ApplyModifiedProperties();
      }
    }

    private void UseEvent()
    {
      GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
      Event.current.Use();
    }
  }
}
