using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;
using Vector2 = UnityEngine.Vector2;

namespace LinePath
{
  public class LinePathCreator : MonoBehaviour
  {
    public List<Vector2> lines = new();

    private float[] allMgntd;
    private float[] mgntds;

    private float maxMgntd;

    private void Awake()
    {
      allMgntd = GetAllMagnitudeArray(true);
      mgntds = GetAllMagnitudeArray(false);
      maxMgntd = GetAllMagnitudeValue();
    }

    private Vector2 GetLinePosition(int index) => (Vector2) transform.position + lines[index];

    public Vector2 GetPosition(float percentage)
    {
      if (lines.Count == 0) throw new Exception("lines cannot be zero.");

      var max = percentage * maxMgntd;
      var index = GetIndex(allMgntd, max);

      var l1 = GetLinePosition(index);
      var l2 = GetLinePosition(index + 1);

      var minus = 0f;
      if (index > 0) minus = allMgntd[index - 1];

      return Vector2.Lerp(l1, l2, (max - minus) / mgntds[index]);
    }

    private float[] GetAllMagnitudeArray(bool sumPrevious)
    {
      var res = new List<float>();
      var tmp = 0f;
      for (var i = 0; i < lines.Count - 1; i++)
      {
        var r = GetLinePosition(i).GetMagnitude(GetLinePosition(i + 1));
        res.Add(r + (sumPrevious ? tmp : 0f));
        tmp += r;
      }

      return res.ToArray();
    }

    private float GetAllMagnitudeValue()
    {
      var res = 0f;
      for (var i = 0; i < lines.Count - 1; i++)
        res += GetLinePosition(i).GetMagnitude(GetLinePosition(i + 1));
      return res;
    }

    private int GetIndex(float[] allMgntd, float value)
    {
      var list = allMgntd.ToList();
      if (value < maxMgntd)
        return list.IndexOf(list.First(x => value < x));
      else
        return list.Count - 1;
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
      if (lines.Count == 0) return;
      Gizmos.color = Color.magenta;

      for (var i = 1; i < lines.Count; i++)
      {
        Gizmos.DrawLine(GetLinePosition(i - 1), GetLinePosition(i));
      }
    }
#endif
  }
}
