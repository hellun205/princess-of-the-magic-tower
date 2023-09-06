using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
  public class Room : MonoBehaviour
  {
    public Transform startPosition;

    public List<Link> links;
    public List<Transform> linkPositions;
    public List<Locker> lockers;

    public List<int> enemies = new List<int>();

    private void Awake()
    {
      startPosition = transform.Find("@start-pos");
      links = transform.Find("@link-to").GetComponentsInChildren<Link>().ToList();
      linkPositions = transform.Find("@link-position").GetComponentsInChildren<Transform>().ToList();
      lockers = transform.Find("@lock").GetComponentsInChildren<Locker>().ToList();

      links.ForEach(link => link.currentRoomName = name);
    }

    public void AddEnemy(int index) {
      enemies.Add(index);
      RefreshLocker();
    }

    public void RemoveEnemy(int index) {
      enemies.Remove(index);
      RefreshLocker();
    }

    public void RefreshLocker() {
      foreach (var locker in lockers ) {
        if (enemies.Any())
          locker.Lock();
        else
          locker.UnLock();
      }
    }
  }
}
