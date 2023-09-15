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
    /// Call when before timer starts
    /// </summary>
    public event TimerEventListener onBeforeStart;

    /// <summary>
    /// Call when before timer ends
    /// </summary>
    public event TimerEventListener onBeforeEnd;

    public event TimerEventListener onForceStop;

    /// <summary>
    /// Call when timer activating
    /// </summary>
    public event TimerEventListener onTick;

    /// <summary>
    /// Current time of timer
    /// </summary>
    public float elapsedTime { get; private set; }

    /// <summary>
    /// End value of timer
    /// </summary>
    public float duration { get; set; }

    public float easePower { get; set; } = 2f;

    /// <summary>
    /// Is working with unscaled delta time
    /// </summary>
    public bool isUnscaled { get; set; }

    public float value { get; private set; }

    public TimerType type { get; set; }

    private Coroutiner coroutiner;

    public Timer(float duration, TimerType type = TimerType.Normal)
    {
      coroutiner = new Coroutiner(Routine);
      this.type = type;
      this.duration = duration;
    }

    public Timer(float duration, TimerEventListener onEndCallback) : this(duration)
    {
      onEnd += onEndCallback;
    }

    private IEnumerator Routine()
    {
      onStart?.Invoke(this);
      while (true)
      {
        elapsedTime += isUnscaled ? Time.unscaledDeltaTime : Time.deltaTime;

        var t = elapsedTime / duration;

        value = type switch
        {
          TimerType.EaseIn => t.EaseIn(easePower),
          TimerType.EaseOut => t.EaseOut(easePower),
          TimerType.EaseInOut => t.EaseInOut(easePower),
          _ => t,
        };

        onTick?.Invoke(this);
        if (elapsedTime >= duration)
        {
          onBeforeEnd?.Invoke(this);
          onEnd?.Invoke(this);
          yield break;
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
      elapsedTime = startValue;
      Resume();
    }

    /// <summary>
    /// Resume this timer
    /// </summary>
    public void Resume()
    {
      onBeforeStart?.Invoke(this);
      coroutiner.Start();
    }

    /// <summary>
    /// Stop this timer
    /// </summary>
    public void Stop()
    {
      coroutiner.Stop();
      onForceStop?.Invoke(this);
    }
  }
}