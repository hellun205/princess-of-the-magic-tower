using UnityEngine;
using Util;

namespace LinePath
{
  public class LineFollower : MonoBehaviour
  {
    public LinePathCreator path;

    public bool isEnabled;

    public Timer timer;
    
    private void Awake()
    {
      timer = new Timer(1f);

      timer.onTick += t => transform.position = path.GetPosition(t.value);
      timer.onEnd += t => t.Start();
    }

    private void Start()
    {
      void loop()
      {
        Utils.WaitUntil(() => isEnabled, () =>
        {
          timer.Resume(); 
          Utils.WaitUntil(() => !isEnabled, () =>
          {
            timer.Stop(); 
            loop();
          });
        });
      }
      
      loop();
    }
  }
}
