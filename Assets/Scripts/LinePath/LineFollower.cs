using UnityEngine;
using Util;

namespace LinePath
{
  [ExecuteInEditMode]
  public class LineFollower : MonoBehaviour
  {
    public LinePathCreator path;

    public float time;

    public bool isEnabled;

    private Timer timer;

    [SerializeField]
    private TimerType type;

    [SerializeField]
    [HideInInspector]
    private bool awake;
    
    private void Awake()
    {
      awake = true;
      timer = new Timer(time, type);
      timer.onBeforeStart += t =>
      {
        t.duration = time;
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
