using System;
using System.Collections.Generic;
using System.Linq;
using Pool;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Map
{
  [ExecuteInEditMode]
  public class Room : MonoBehaviour
  {
    [NonSerialized]
    public Transform startPosition;

    [NonSerialized]
    public List<Link> links;

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

    public bool isCleared = false;

#if UNITY_EDITOR
    [SerializeField]
    private Sprite[] backgrounds;

    [SerializeField]
    private Color[] bgColors;

    [ContextMenu("Apply Background layers")]
    private void ApplyBackgroundLayers()
    {
      var bg = transform.Find("@backgrounds");

      foreach (var obj in bg.GetComponentsInChildren<SpriteRenderer>())
        DestroyImmediate(obj.gameObject);

      for (var i = 0; i < backgrounds.Length; i++)
      {
        var newGo = new GameObject($"layer{i}", typeof(SpriteRenderer));
        newGo.transform.localScale = Vector3.one;
        newGo.transform.localPosition = Vector3.one;
        var sr = newGo.GetComponent<SpriteRenderer>();
        sr.sprite = backgrounds[i];
        sr.sortingLayerName = "Room";
        sr.sortingOrder = i;
        if (bgColors.Length >= i + 1)
          sr.color = bgColors[i];
        newGo.transform.SetParent(bg.transform);
      }
    }
#endif

    private void Awake()
    {
      startPosition = transform.Find("@start-pos");
      links = transform.Find("@link-to").GetComponentsInChildren<Link>().ToList();
      linkPositions = transform.Find("@link-position").GetComponentsInChildren<Transform>().ToList();
      doors = transform.Find("@door").GetComponentsInChildren<Door.Door>().ToList();
      summoners = transform.Find("@summon").GetComponentsInChildren<PoolSummoner>().ToList();
      walls = transform.Find("@walls").GetComponents<Collider2D>().ToList();
      objects = transform.Find("@objects").GetComponentsInChildren<Transform>().Select(x => x.gameObject).ToList();

      links.ForEach(link => link.currentRoomName = name);
      summoners.ForEach(summoner => summoner.room = name);
      doors.ForEach(door => door.room = this);
    }

    public void AddEnemy(int index)
    {
      enemies.Add(index);
    }

    public void OnEntered()
    {
      if (isCleared) return;

      summoners.ForEach(summoner => summoner.Summon());
      doors.ForEach(door => door.OnEntered());
    }
  }
}
