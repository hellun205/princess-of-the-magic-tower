using System;
using UnityEngine;

namespace Managers
{
  [Serializable]
  public class SaveData
  {
    public string stage;

    public string room;

    public string[] cleared;

    public Vector2 position;
  }
}
