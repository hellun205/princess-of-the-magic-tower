using System;
using UnityEngine;

namespace Pool.Extension
{
  [RequireComponent(typeof(Follower))]
  public class FollowingText : Text
  {
    [NonSerialized]
    public Follower follow;
    
    protected override void Awake()
    {
      base.Awake();
      follow = GetComponent<Follower>();
    }
  }
}
