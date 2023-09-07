using System.Collections;
using UnityEngine;

namespace Dialogue
{
  /// <summary>
  /// 대사 중 잠시 쉽니다.
  /// </summary>
  public class DWait : IDialogueContent
  {
    private float time { get; }
    
    /// <param name="time">시간</param>
    public DWait(float time)
    {
      this.time = time;
    }

    public IEnumerator Active()
    {
      yield return new WaitForSecondsRealtime(time);
    }
  }
}
