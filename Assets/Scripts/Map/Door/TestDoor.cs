using DG.Tweening;
using UnityEngine;

namespace Map.Door
{
  public class TestDoor : Door
  {
    [SerializeField]
    private SpriteRenderer sr;

    public override void Close()
    {
      sr.DOFade(1f, 1f).OnComplete(() => base.Close());
    }

    public override void Open()
    {
      sr.DOFade(0f, 1f).OnComplete(() => base.Open());
    }
  }
}
