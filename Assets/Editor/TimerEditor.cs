using System;
using UnityEditor;
using UnityEngine;
using Util;

namespace Editor
{
  [CustomPropertyDrawer(typeof(Timer))]
  public class TimerEditor : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

      var indent = EditorGUI.indentLevel;
      EditorGUI.indentLevel = 0;

      var isUnscaledRect = new Rect(position.x, position.y, 20, 18f);
      var isUnscaledLabelRect = new Rect(position.x + 20, position.y, 60, 18f);
      var typeRect = new Rect(position.x + 80, position.y, 90, 18f);
      var easePowLabelRect = new Rect(position.x + 175, position.y, 45, 18f);
      var easePowRect = new Rect(position.x + 220, position.y, position.width - 225, 18f);
      var durationLabelRect = new Rect(position.x, position.y + 20f, 50, 18f);
      var durationRect = new Rect(position.x + 60f, position.y + 20f, position.width - 80, 18f);
      var durationSecondRect = new Rect(position.x + durationRect.width + 65f, position.y + 20f, 20, 18f);
      var elapsedTimeLabelRect = new Rect(position.x, position.y + 50,130, 18f);
      var valueLabelRect = new Rect(position.x, position.y + 70, 130, 18f);
      var stateLabelRect = new Rect(position.x, position.y + 90, 130 , 18f);
      var elapsedTimeRect = new Rect(position.x + 130, position.y + 50, position.width - 130, 18f);
      var valueRect = new Rect(position.x + 130, position.y + 70, position.width - 130, 18f);
      var stateRect = new Rect(position.x + 130, position.y + 90, position.width - 130, 18f);

      EditorGUI.LabelField(isUnscaledLabelRect, "unscaled");
      EditorGUI.PropertyField(isUnscaledRect, property.FindPropertyRelative("m_isUnscaled"), GUIContent.none);
      EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("m_type"), GUIContent.none);
      if (property.FindPropertyRelative("m_type").enumValueIndex is 1 or 2 or 3)
      {
        EditorGUI.LabelField(easePowLabelRect, "power");
        EditorGUI.PropertyField(easePowRect, property.FindPropertyRelative("m_easePower"), GUIContent.none);
      }

      EditorGUI.LabelField(durationLabelRect, "duration");
      EditorGUI.PropertyField(durationRect, property.FindPropertyRelative("m_duration"), GUIContent.none);
      EditorGUI.LabelField(durationSecondRect, "s");

      EditorGUI.LabelField(elapsedTimeLabelRect, "elapsed time");
      EditorGUI.LabelField(elapsedTimeRect,
        $"{Math.Round(property.FindPropertyRelative("m_elapsedTime").floatValue, 2)} s");
      EditorGUI.LabelField(valueLabelRect, "value");
      EditorGUI.LabelField(valueRect,
        $"{Math.Round(property.FindPropertyRelative("m_value").floatValue, 2) * 100} %");

      var state = property.FindPropertyRelative("m_isPlaying").boolValue;
      
      EditorGUI.LabelField(stateLabelRect, "current timer state");
      EditorGUI.LabelField(stateRect, state ? "PLAYING" : "STOP");
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      return base.GetPropertyHeight(property, label) + 20f * 5;
    }
  }
}