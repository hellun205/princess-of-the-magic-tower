using System;
using System.Diagnostics;
using Managers;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Ranking
{
  public class StopwatchObject : MonoBehaviour
  {
    private float m_elapsed;

    public float elapsed
    {
      get => (float) Math.Round(m_elapsed, 2);
      set => m_elapsed = value;
    } 

    private TextMeshProUGUI text;

    public bool isRunning;

    private void Awake()
    {
      text = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
      if (isRunning)
        m_elapsed += Time.deltaTime;
      
      var elapsedTime = elapsed;

      var m = Mathf.FloorToInt(elapsedTime) / 60;
      var s = Mathf.FloorToInt(elapsedTime) % 60;
      var p = Mathf.FloorToInt((elapsedTime - Mathf.FloorToInt(elapsedTime)) * 100);
      
      text.text = $"{m:d2}:{s:d2}.{p:d2}";
      GameManager.record = elapsed;
    }
  }
}
