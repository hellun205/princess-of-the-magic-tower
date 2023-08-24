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

    private void Awake()
    {
      startPosition = transform.Find("@start-pos");
      links = transform.Find("@link-to").GetComponentsInChildren<Link>().ToList();
      linkPositions = transform.Find("@link-position").GetComponentsInChildren<Transform>().ToList();

      links.ForEach(link => link.currentRoomName = name);
    }
  }
}
