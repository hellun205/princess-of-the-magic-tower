using System.Collections;
using UnityEngine;

namespace Util
{
  public class Timer
  {
    public delegate void TimerEventListener(Timer sender);

    /// <summary>
    /// Call when timer ends
    /// </summary>
    public event TimerEventListener onEnd;
    
    /// <summary>
    /// Call when timer starts
    /// </summary>
    public event TimerEventListener onStart;
    
    /// <summary>
    /// Current time of timer
    /// </summary>
    public float currentTime { get; private set; }

    /// <summary>
    /// End value of timer
    /// </summary>
    public float time { get; set; }

    /// <summary>
    /// Is working with unscaled delta time
    /// </summary>
    public bool isUnscaled;

    private Coroutiner coroutiner;

    public Timer(float time)
    {
      coroutiner = new Coroutiner(Routine);
      this.time = time;
    }

    public Timer(float time, TimerEventListener onEndCallback) : this(time)
    {
      onEnd += onEndCallback;
    }
    
    private IEnumerator Routine()
    {
      onStart?.Invoke(this);
      while (true)
      {
        currentTime += isUnscaled ? Time.unscaledDeltaTime : Time.deltaTime;

        if (currentTime >= time)
        {
          Stop();
          onEnd?.Invoke(this);
        }
        yield return new WaitForEndOfFrame();
      }
    }

    /// <summary>
    /// Start this timer
    /// </summary>
    /// <param name="startValue">starting value</param>
    public void Start(float startValue = 0f)
    {
      currentTime = startValue;
      coroutiner.Start();
    }

    /// <summary>
    /// Stop this timer
    /// </summary>
    public void Stop()
    {
      coroutiner.Stop();
    }
  }
}
