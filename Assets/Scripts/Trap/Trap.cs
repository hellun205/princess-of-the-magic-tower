using System;
using Map;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Trap
{
  public abstract class Trap : MonoBehaviour, IRoomEnterEventHandler, IRoomExitEventHandler
  {
    [Header("Trap")]
    public TrapCondition activeOn;

    public bool startState;

    protected bool currentState;

    [Header("Active On Detect Player")]
    [SerializeField]
    private LayerMask detectTarget;

    private Detector detector;

    private bool isDetect;

    [Header("Active On Repeat")]
    [SerializeField]
    private float repeatDelay;

    [SerializeField]
    private float startDelay;

    [SerializeField]
    private float deactivateDelay;

    [SerializeField]
    private Timer repeatTimer;

    private bool isRoomEntered;

    protected virtual void Awake()
    {
      detector = transform.GetComponentInChildren<Detector>();

      if ((activeOn & TrapCondition.Detect) != 0)
      {
        detector.SetTarget(detectTarget);
        detector.onEnter += _ =>
        {
          isDetect = true;
          if (!currentState) Activate();
        };
        detector.onExit += _ =>
        {
          isDetect = false;
          if (currentState) Deactivate();
        };
      }

      if ((activeOn & TrapCondition.Repeat) != 0)
      {
        repeatTimer = new Timer(repeatDelay);
        repeatTimer.onEnd += OnTimerEnd;
        repeatTimer.onBeforeStart += t => t.duration = repeatDelay;
      }
    }

    private void OnTimerEnd(Timer sender)
    {
      if (!currentState) Activate();
      Utils.WaitUntil(() => isRoomEntered, () =>
      {
        Utils.Wait(deactivateDelay, () =>
        {
          if (isDetect)
            Utils.WaitUntil(() => !isDetect, () =>
            {
              Deactivate();
              sender.Start();
            });
          else
          {
            if (currentState)
              Deactivate();
            sender.Start();
          }
        });
      });
    }

    protected virtual void Activate()
    {
      currentState = true;
    }

    protected virtual void Deactivate()
    {
      currentState = false;
    }

    public void OnRoomEntered()
    {
      isRoomEntered = true;
      if (startState)
        Activate();
      else
        Deactivate();

      if ((activeOn & TrapCondition.Repeat) != 0)
        Utils.Wait(startDelay, () => repeatTimer.Start());
    }

    public void OnRoomExited()
    {
      isRoomEntered = false;
      if (repeatTimer.isPlaying) repeatTimer.Stop();
      Deactivate();
      
    }
  }
}
