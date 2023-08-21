using System;
using UnityEngine;

namespace Map
{
  public class Room : MonoBehaviour
  {
    public Transform startPosition;

    private void Awake()
    {
      startPosition = transform.Find("@start-pos");
    }
  }
}
