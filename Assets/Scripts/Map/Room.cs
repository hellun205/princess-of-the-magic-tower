using System;
using System.Collections.Generic;
using System.Linq;
using Pool;
using UnityEngine;

namespace Map
{
  public class Room : MonoBehaviour
  {
    public Transform startPosition;

    public List<Link> links;
    public List<Transform> linkPositions;
    public List<Door.Door> doors;
    public List<PoolSummoner> summoners;

    public bool doorEnable = true;

    public List<int> enemies = new List<int>();

    public bool isCleared = false;

    private void Awake()
    {
      startPosition = transform.Find("@start-pos");
      links = transform.Find("@link-to").GetComponentsInChildren<Link>().ToList();
      linkPositions = transform.Find("@link-position").GetComponentsInChildren<Transform>().ToList();
      doors = transform.Find("@door").GetComponentsInChildren<Door.Door>().ToList();
      summoners = transform.Find("@summon").GetComponentsInChildren<PoolSummoner>().ToList();

      links.ForEach(link => link.currentRoomName = name);
      summoners.ForEach(summoner => summoner.room = name);
      doors.ForEach(door => door.room = this);
    }

    public void AddEnemy(int index)
    {
      enemies.Add(index);
      // SetDoorState(true);
    }

    public void OnEntered()
    {
      if (isCleared) return;
      
      doors.ForEach(door => door.OnEntered());
      summoners.ForEach(summoner => summoner.Summon());
    }

    // public void SetDoorState(bool state)
    // {
    //   if (doorEnable == state) return;
    //   
    //   doorEnable = state;
    //   foreach (var door in doors)
    //     if (state)
    //       door.Close();
    //     else
    //       door.Open();
    // }
  }
}