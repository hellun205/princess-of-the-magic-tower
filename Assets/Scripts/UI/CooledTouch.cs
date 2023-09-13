using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Util;

namespace UI
{
  public class CooledTouch : Touch
  {
    [Header("Cooldown")]
    [SerializeField]
    private Image targetImage;

    public float cooldown;

    private Timer timer;

    public bool isEnabled = true;

    [NonSerialized]
    public bool isCooldown;

    protected override void Awake()
    {
      base.Awake();
      timer = new Timer(cooldown);
      timer.onBeforeStart += OnTimerBeforeStart;
      timer.onStart += OnTimerStart;
      timer.onTick += OnTimerTick;
      timer.onEnd += OnTimerEnd;
      onTouch += OnTouch;
    }

    private void OnTimerBeforeStart(Timer sender)
    {
      sender.time = cooldown;
    }

    private void OnTouch(PointerEventData eventData)
    {
      if (isEnabled)
        timer.Start();
    }

    private void OnTimerStart(Timer sender)
    {
      isCooldown = true;
      interactable = false;
    }

    private void OnTimerTick(Timer sender)
    {
      targetImage.fillAmount = 1 - sender.normalized;
    }

    private void OnTimerEnd(Timer sender)
    {
      isCooldown = false;
      interactable = true;
      targetImage.fillAmount = 0f;
    }
  }
}