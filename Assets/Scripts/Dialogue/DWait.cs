using System.Collections;
using UnityEngine;

namespace Dialogue
{
  public class DWait : IDialogueContent
  {
    private float time { get; }
    
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
