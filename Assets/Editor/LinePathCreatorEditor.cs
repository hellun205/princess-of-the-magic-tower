using System.Collections.Generic;
using LinePath;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Util;

namespace Editor
{
  [CustomEditor(typeof(LinePathCreator))]
  public class LinePathCreatorEditor : UnityEditor.Editor
  {
    private LinePathCreator _this;
    private SerializedProperty sp_lines;
    private ReorderableList rl_lines;
    private SerializedProperty sp_drawIcon;
    private bool isEditing;
    private int curIndex;
    private List<SerializedProperty> sp_lineList = new();
    private bool shift;

    private void OnEnable()
    {
      _this = target as LinePathCreator;
      sp_lines = serializedObject.FindProperty("lines");
      sp_drawIcon = serializedObject.FindProperty("drawIcon");
    }

    private void OnDisable()
    {
      isEditing = false;
    }

    public override void OnInspectorGUI()
    {
      serializedObject.Update();

      isEditing = GUILayout.Toggle(isEditing, "Edit Path", EditorStyles.miniButton);
      if (GUILayout.Button("Clear", EditorStyles.miniButton))
      {
        sp_lines.ClearArray();
      }

      sp_drawIcon.boolValue = isEditing;

      if (isEditing)
      {
        var style = new GUIStyle()
        {
          fontStyle = FontStyle.Italic,
          normal = new()
          {
            textColor = Color.gray
          },
          wordWrap = true
        };
        EditorGUILayout.LabelField("Left Click: Create or Move path (press shift to fixed axis)", style);  
        EditorGUILayout.LabelField("Right Click: Remove path", style);  
      }
      
      EditorGUILayout.PropertyField(serializedObject.FindProperty("lines"));
      EditorUtility.SetDirty(_this);
      serializedObject.ApplyModifiedProperties();
    }


    private void OnSceneGUI()
    {
      if (!isEditing) return;
      var e = Event.current;
      var mousePos = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;

      if (e.type == EventType.MouseDown && e.button == 0)
      {
        if (sp_lines.arraySize == 0)
        {
          serializedObject.Update();
          sp_lines.InsertArrayElementAtIndex(sp_lines.arraySize);
          sp_lines.GetArrayElementAtIndex(sp_lines.arraySize - 1).vector2Value = mousePos - _this.transform.position;
          serializedObject.ApplyModifiedProperties();
          curIndex = sp_lines.arraySize - 1;
          UseEvent();
          return;
        }

        sp_lineList.Clear();
        for (var i = 0; i < sp_lines.arraySize; i++)
          sp_lineList.Add(sp_lines.GetArrayElementAtIndex(i));

        for (var i = 0; i < sp_lineList.Count; i++)
        {
          var pos = (Vector2) _this.transform.position + sp_lineList[i].vector2Value;
          if ((pos - (Vector2) mousePos).magnitude <= 0.5f)
          {
            curIndex = i;
            break;
          }

          curIndex = -1;
        }

        if (curIndex == -1)
        {
          serializedObject.Update();
          sp_lines.InsertArrayElementAtIndex(sp_lines.arraySize);
          sp_lines.GetArrayElementAtIndex(sp_lines.arraySize - 1).vector2Value = mousePos - _this.transform.position;
          serializedObject.ApplyModifiedProperties();
          curIndex = sp_lines.arraySize - 1;
        }

        UseEvent();
      }
      else if (e.type == EventType.MouseDown && e.button == 1)
      {
        for (var i = 0; i < sp_lines.arraySize; i++)
        {
          var pos = (Vector2) _this.transform.position + sp_lines.GetArrayElementAtIndex(i).vector2Value;
          if ((pos - (Vector2) mousePos).magnitude <= 0.5f)
          {
            serializedObject.Update();
            sp_lines.DeleteArrayElementAtIndex(i);
            serializedObject.ApplyModifiedProperties();
            break;
          }
        }

        UseEvent();
      }

      if (e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftShift)
        shift = true;
      else if (e.type == EventType.KeyUp && e.keyCode == KeyCode.LeftShift)
        shift = false;
      
      if (e.type == EventType.MouseDrag && e.button == 0)
      {
        if (curIndex != -1)
        {
          serializedObject.Update();
          var child = sp_lines.GetArrayElementAtIndex(curIndex);

          var pos = mousePos - _this.transform.position;
          var to = pos;
          
          if (shift && sp_lines.arraySize > 1)
          {
            var previous = sp_lines.GetArrayElementAtIndex(curIndex - 1).vector2Value;
            if (previous.x - 3f < pos.x && previous.x + 3f > pos.x)
              to = to.Setter(x: previous.x);
            else if (previous.y - 3f < pos.y && previous.y + 3f > pos.y)
              to = to.Setter(y: previous.y);
          }

          child.vector2Value = to;

          EditorUtility.SetDirty(_this);
          serializedObject.ApplyModifiedProperties();

          UseEvent();
        }
      }
    }

    private void UseEvent()
    {
      GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
      Event.current.Use();
    }
  }
}
