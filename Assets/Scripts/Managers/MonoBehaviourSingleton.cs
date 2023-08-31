﻿using System;
using UnityEngine;

namespace Managers
{
  public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
  {
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
      if (Instance is null)
        Instance = (T)this;
      else
      {
        Destroy(this);
        throw new Exception($"already exists object: {this.name}");
      }
      
      if (this is IDontDestoryObject)
        DontDestroyOnLoad(gameObject);
    }
  }
}