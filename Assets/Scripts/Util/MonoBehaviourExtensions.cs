using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Util
{
  public static class MonoBehaviourExtensions {
    /// <summary>
    /// Performs ForceRebuildLayoutImmediate some number of times based on how
    /// many nested layout panels we have.
    /// </summary>
    public static void RebuildLayout(this MonoBehaviour b, int layoutNesting = 1) {
      b.StartCoroutine(RebuildLayout(b.transform as RectTransform, layoutNesting));
    }
 
    /// <summary>
    /// This seems unhinged but it covers all our bases.
    ///
    /// Unity layout is trash: a single rebuild call is ineffective for nested
    /// content size fitters or layout panels that depend on them. And it
    /// (sometimes, but not always) needs to wait a frame before rebuilding
    /// will have an effect. We *also* don't want UI flicker from doing this
    /// async, so we rebuild once before waiting, and then again after waiting.
    /// </summary>
    private static IEnumerator RebuildLayout(RectTransform rectTransform, int iterations) {
      for (int i = 0; i < iterations; i++) {
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
      }
 
      yield return new WaitForEndOfFrame();
 
      for (int i = 0; i < iterations; i++) {
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
      }
    }
  }
}
