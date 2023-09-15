using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace LinePath
{
  public class LineFollower : MonoBehaviour
  {
    public LinePathCreator path;

    public float duration;

    public bool isEnabled;

    private Timer timer;

    public TimerType type;

    [SerializeField]
    [HideInInspector]
    private bool awake;
    
    private void Awake()
    {
      awake = true;
      timer = new Timer(duration, type);
      timer.onBeforeStart += t =>
      {
        t.duration = duration;
        t.type = type;
      };
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
