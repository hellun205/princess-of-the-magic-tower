using System.Collections;
using Managers;
using UnityEngine;

namespace Dialogue
{
  public class DText : IDialogueContent
  {
    private string text { get; }
    private float speed { get; }

    public DText(string text, float speed = 0.07f)
    {
      this.text = text;
      this.speed = speed;
    }

    public IEnumerator Active()
    {
      var dm = GameManager.Dialogue;

      foreach (var chr in text.ToCharArray())
      {
        dm.AppendText(chr);

        yield return chr == ' ' ? null : new WaitForSecondsRealtime(speed);
      }
    }
  }
}
