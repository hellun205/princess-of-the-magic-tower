using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Util
{
  [Serializable]
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

    [SerializeField]
    private bool m_isUnscaled;

    [SerializeField]
    private float m_elapsedTime;

    [SerializeField]
    [Min(0.1f)]
    private float m_duration;

    [SerializeField]
    private float m_easePower = 2f;

    [SerializeField]
    [Range(0f, 1f)]
    private float m_value;

    [SerializeField]
    private TimerType m_type;

    [SerializeField]
    private bool m_isPlaying;

    /// <summary>
    /// Current time of timer
    /// </summary>
    public float elapsedTime
    {
      get => m_elapsedTime;
      private set => m_elapsedTime = value;
    }

    /// <summary>
    /// End value of timer
    /// </summary>
    public float duration
    {
      get => m_duration;
      set => m_duration = value;
    }

    public float easePower
    {
      get => m_easePower;
      set => m_easePower = value;
    }

    /// <summary>
    /// Is working with unscaled delta time
    /// </summary>
    public bool isUnscaled
    {
      get => m_isUnscaled;
      set => m_isUnscaled = value;
    }

    public float value
    {
      get => m_value;
      private set => m_value = value;
    }

    public TimerType type
    {
      get => m_type;
      set => m_type = value;
    }

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
        m_isPlaying = true;
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
          m_isPlaying = false;
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
      m_isPlaying = false;
      onForceStop?.Invoke(this);
    }
  }
}