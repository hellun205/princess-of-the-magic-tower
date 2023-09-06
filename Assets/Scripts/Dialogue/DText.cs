using System.Collections;
using Managers;
using UnityEngine;

namespace Dialogue
{
  /// <summary>
  /// 대사에 문구를 추가합니다.
  /// </summary>
  public class DText : IDialogueContent
  {
    public const float DefaultSpeed = 0.07f; 
    private string text { get; }
    private float speed { get; }
    
    /// <param name="text">문구</param>
    /// <param name="speed">속도</param>
    public DText(string text, float speed = DefaultSpeed)
    {
      this.text = text;
      this.speed = speed;
    }

    public IEnumerator Active()
    {
      foreach (var chr in text.ToCharArray())
      {
        GameManager.Dialogue.AppendText(chr);

        yield return chr == ' ' ? null : new WaitForSecondsRealtime(speed);
      }
    }
  }
}
