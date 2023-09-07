using System;
using DG.Tweening;
using UnityEngine;

namespace Map
{
  public class Door : MonoBehaviour
  {
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private BoxCollider2D col;

    public void Close()
    {
      sr.DOFade(1f, 1f).OnComplete(() => { col.enabled = true; });
    }

    public void Open()
    {
      sr.DOFade(0f, 1f).OnComplete(() => { col.enabled = false; });
    }
  }
}