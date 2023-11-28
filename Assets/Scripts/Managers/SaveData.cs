using System;
using Ranking;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
  [Serializable]
  public class SaveData
  {
    public string stage;

    public string room;

    public string[] cleared;

    public Vector2 position;

    public string objectName;

    public string nickname;

    public float record;

    public int death;

    public ProfileImage profile;
  }
}
