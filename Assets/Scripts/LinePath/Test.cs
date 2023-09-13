using System;
using System.Collections;
using UnityEngine;

namespace LinePath
{
  public class Test : MonoBehaviour
  {
    public LinePathCreator path;

    public Transform target;
    
    public float t = 0;

    public bool isSlerp;

    private void Start()
    {
    }

    private void Update()
    {
      t += Time.deltaTime;
      target.position = path.GetPosition(t);
      if (t >= 1) t = 0;
    }
  }
}
