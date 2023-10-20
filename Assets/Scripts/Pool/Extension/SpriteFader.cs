using System.Collections;
using Managers;
using UnityEngine;

namespace Pool.Extension
{
  public class SpriteFader : MonoBehaviour
  {
    public int count;
    public float interval;

    public float fadeDuration;
    public float startAlpha;

    public bool flip;

    public SpriteRenderer spriteRenderer;

    public void Play()
    {
      StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
      for (var i = 0; i < count; i++)
      {
        var fade = GameManager.Pool.Summon<SpriteFade>("effect/fade", transform.position);

        fade.startAlpha = startAlpha;
        fade.fadeDuration = fadeDuration;
        fade.SetSprite(spriteRenderer);
        fade.sr.flipX = flip;
        fade.Play();

        yield return new WaitForSeconds(interval);
      }
    }
  }
}