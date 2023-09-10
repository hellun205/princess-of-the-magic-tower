using System;
using DG.Tweening;
using UnityEngine;

namespace Interact.Object
{
  public class Obstacle : InteractiveObject
  {
    [Header("Obstacle")]
    public int hp;

    public int destoryLevel;

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Collider2D col;

#if UNITY_EDITOR
    private void Reset()
    {
      condition = InteractCondition.Attack;
      type = InteractType.First;
    }
#endif

    protected override void OnInteract(GameObject caster)
    {
      if (!caster.TryGetComponent(typeof(IObstacleDestroyable), out var component)) return;
      if (destoryLevel > (component as IObstacleDestroyable).destroyLevel) return;
      if (--hp <= 0) OnBreak();
    }

    private void OnBreak()
    {
      col.enabled = false;
      sr.DOFade(0f, 1f).OnComplete(() => gameObject.SetActive(false));
    }
  }
}
