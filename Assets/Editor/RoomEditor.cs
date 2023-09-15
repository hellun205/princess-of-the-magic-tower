using System;
using System.Linq;
using Map;
using UnityEditor;
using UnityEngine;

namespace Editor
{
  [CustomEditor(typeof(Room)), CanEditMultipleObjects]
  public class RoomEditor : UnityEditor.Editor
  {
    private Room[] _targets;

    private void OnEnable()
    {
      _targets = targets.Cast<Room>().ToArray();
    }

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      if (GUILayout.Button("Apply Resource", EditorStyles.miniButton))
      {
        foreach (var _target in _targets)
        {
          _target.ApplyBackgroundLayers();
          _target.ApplyDoorSprites();
          Debug.Log($"Applyed Resource: {_target.name}");
        }
      }
    }
  }
}
