using DG.Tweening;
using UnityEngine;
using Util;

namespace Pool.Extension
{
  public class SpriteFade : UsePool
  {
    public float startAlpha;
    public float fadeDuration;

    public SpriteRenderer sr;

    protected override void Awake()
    {
      base.Awake();
      sr = GetComponent<SpriteRenderer>();
    }

    protected override void OnSummon()
    {
      
    }

    public void SetSprite(SpriteRenderer targetSr)
    {
      sr.sprite = targetSr.sprite;
      sr.color = targetSr.color;
      sr.sortingLayerID = targetSr.sortingLayerID;
      sr.sortingOrder = targetSr.sortingOrder - 1;
    }

    public void Play()
    {
      sr.color = sr.color.Setter(a: startAlpha);
      sr.DOFade(0f, fadeDuration).OnComplete(() => pool.Release());
    }
  }
}