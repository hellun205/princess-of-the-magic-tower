using System;
using Cinemachine;
using Managers;
using UnityEngine;

namespace Map
{
  public class CameraController : MonoBehaviour
  {
    [NonSerialized]
    public CinemachineVirtualCamera virtualCamera;

    [NonSerialized]
    public CinemachineConfiner2D confiner2D;

    [NonSerialized]
    public CinemachineFollowZoom followZoom;
    
    private void Awake()
    {
      virtualCamera = transform.GetComponentInChildren<CinemachineVirtualCamera>();
      confiner2D = virtualCamera.GetComponent<CinemachineConfiner2D>();
      followZoom = virtualCamera.GetComponent<CinemachineFollowZoom>();
      DontDestroyOnLoad(gameObject);
      
      // SetTarget(GameManager.Player.transform);

      GameManager.OnLoaded += () => SetTarget(GameManager.Player.transform);
    }

    public void SetTarget(Transform target)
    {
      virtualCamera.m_Follow = target;
    }

    public void SetZoom(float value)
    {
      followZoom.m_Width = value;
    }
  }
}
