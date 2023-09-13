using UnityEngine;
using Util;

namespace LinePath
{
  public class LineFollower : MonoBehaviour
  {
    public LinePathCreator path;

    public float time;

    public bool isEnabled;

    private Timer timer;

    private void Awake()
    {
      timer = new Timer(time);
      timer.onBeforeStart += t => t.time = time;
      timer.onTick += t => transform.position = path.GetPosition(t.normalized);
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
