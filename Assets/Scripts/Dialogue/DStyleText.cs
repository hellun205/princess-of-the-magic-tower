using System.Collections;
using System.Collections.Generic;
using System.Text;
using Managers;
using UnityEngine;

namespace Dialogue
{
  /// <summary>
  /// 대사에 Tag를 포함한 문구를 추가합니다.
  /// </summary>
  public class DStyleText : IDialogueContent
  {
    public string text;
    public float speed;

    public List<(string tag, string value)> styles = new();
    
    /// <param name="text">문구</param>
    /// <param name="styleTag">태그 이름</param>
    /// <param name="styleValue">태그 값</param>
    /// <param name="speed">속도</param>
    public DStyleText(string text, string styleTag, string styleValue = "", float speed = DText.DefaultSpeed) :
      this(text, speed)
    {
      styles.Add((styleTag, styleValue));
    }
    
    /// <param name="text">문구</param>
    /// <param name="styleTag">태그 이름</param>
    /// <param name="speed">속도</param>
    public DStyleText(string text, string styleTag, float speed = DText.DefaultSpeed) : this(text, speed)
    {
      styles.Add((styleTag, ""));
    }
    
    /// <param name="text">문구</param>
    /// <param name="speed">속도</param>
    public DStyleText(string text, float speed = DText.DefaultSpeed)
    {
      this.text = text;
      this.speed = speed;
    }

    /// <summary>
    /// 태그를 추가합니다.
    /// </summary>
    /// <param name="tag">태그 이름</param>
    /// <param name="value">값</param>
    /// <returns></returns>
    public DStyleText AddTag(string tag, string value = "")
    {
      styles.Add((tag, value));
      return this;
    }

    public IEnumerator Active()
    {
      // Append Tags
      var sb = new StringBuilder();

      for (var i = 0; i < styles.Count; i++)
        sb.Append($"<{styles[i].tag}{(string.IsNullOrEmpty(styles[i].value) ? "" : $"={styles[i].value}")}>");
      var startTag = sb.ToString();

      sb.Clear();

      for (var i = styles.Count - 1; i >= 0; i--)
        sb.Append($"</{styles[i].tag}>");
      var endTag = sb.ToString();

      // Display Text with tags
      GameManager.Dialogue.AppendText(startTag);
      foreach (var chr in text.ToCharArray())
      {
        GameManager.Dialogue.AppendText(chr);
        yield return chr == ' ' ? null : new WaitForSecondsRealtime(speed);
      }
      GameManager.Dialogue.AppendText(endTag);
    }
  }
}