using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Pool;
using UnityEngine;

namespace Map
{
  [ExecuteInEditMode]
  public class Room : MonoBehaviour
  {
    [NonSerialized]
    public Transform startPosition;

    [NonSerialized]
    public List<Transform> linkPositions;

    [NonSerialized]
    public List<Door.Door> doors;

    [NonSerialized]
    public List<PoolSummoner> summoners;

    [NonSerialized]
    public List<Collider2D> walls;

    [NonSerialized]
    public List<GameObject> objects;

    [NonSerialized]
    public List<int> enemies = new List<int>();

    [NonSerialized]
    public Collider2D confinerBound;

    private IRoomEnterEventHandler[] enterEventHandlers;
    private IRoomExitEventHandler[] exitEventHandlers;

    [Header("Room")]
    public bool isCleared = false;

    public RoomResource resourceData;

#if UNITY_EDITOR
    public void ApplyBackgroundLayers()
    {
      var bg = transform.Find("@backgrounds");

      foreach (var obj in bg.GetComponentsInChildren<SpriteRenderer>())
        DestroyImmediate(obj.gameObject);

      for (var i = 0; i < resourceData.backgrounds.Length; i++)
      {
        var newGo = new GameObject($"layer{i}", typeof(SpriteRenderer));
        newGo.transform.localScale = Vector3.one;
        newGo.transform.position = transform.position;
        var sr = newGo.GetComponent<SpriteRenderer>();
        sr.sprite = resourceData.backgrounds[i];
        sr.sortingLayerName = "Room";
        sr.sortingOrder = i;
        if (resourceData.bgColors.Length >= i + 1)
          sr.color = resourceData.bgColors[i];
        newGo.transform.SetParent(bg.transform);
      }
    }

    public void ApplyDoorSprites()
    {
      transform.Find("@door").GetComponentsInChildren<Door.Door>().ToList()
        .ForEach(door =>
          door.GetComponent<SpriteRenderer>().sprite = resourceData.doorResource.animateSprite[door.direction][0]
        );
    }
#endif

    private void Awake()
    {
      startPosition = transform.Find("@start-pos");
      linkPositions = transform.Find("@link-position").GetComponentsInChildren<Transform>().ToList();
      doors = transform.Find("@door").GetComponentsInChildren<Door.Door>().ToList();
      summoners = transform.Find("@summon").GetComponentsInChildren<PoolSummoner>().ToList();
      walls = transform.Find("@walls").GetComponents<Collider2D>().ToList();
      objects = transform.Find("@objects").GetComponentsInChildren<Transform>().Select(x => x.gameObject).ToList();
      confinerBound = transform.Find("@confiner-bounding").GetComponent<Collider2D>();
      enterEventHandlers =
        transform.GetComponentsInChildren(typeof(IRoomEnterEventHandler)).Cast<IRoomEnterEventHandler>().ToArray();
      exitEventHandlers =
        transform.GetComponentsInChildren(typeof(IRoomExitEventHandler)).Cast<IRoomExitEventHandler>().ToArray();

      var requireRooms = transform.GetComponentsInChildren(typeof(IRequireRoom)).Cast<IRequireRoom>();
      foreach (var require in requireRooms)
        require.room = this;
    }

    public void AddEnemy(int index)
    {
      enemies.Add(index);
    }

    public void OnEntered()
    {
      GameManager.Camera.confiner2D.m_BoundingShape2D = confinerBound;
      if (resourceData.changeZoom)
        GameManager.Camera.SetZoom(resourceData.zoom);

      foreach (var handler in enterEventHandlers)
        handler.OnRoomEntered();
    }

    public void OnExited()
    {
      foreach (var handler in exitEventHandlers)
        handler.OnRoomExited();
    }
  }
}