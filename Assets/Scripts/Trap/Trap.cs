using System;
using UnityEngine;
using Util;

namespace Trap
{
  public abstract class Trap : MonoBehaviour
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
    private float deactivateDelay;

    private Timer timer;

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
        timer = new Timer(repeatDelay, t =>
        {
          if (!currentState) Activate();
          Utils.Wait(deactivateDelay, () =>
          {
            if (!isDetect && currentState) Deactivate();
            if (isDetect)
              Utils.WaitUntil(() => !isDetect, () =>
              {
                Deactivate();
                t.Start();
              });
            else
              t.Start();
          });
        });
        timer.Start();
      }
    }

    protected virtual void Activate()
    {
      currentState = true;
    }

    protected virtual void Deactivate()
    {
      currentState = false;
    }

    protected virtual void Start()
    {
      if (startState)
        Activate();
      else
        Deactivate();
    }
  }
}
