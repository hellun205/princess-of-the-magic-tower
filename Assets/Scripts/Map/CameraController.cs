using System;
using Cinemachine;
using Managers;
using UnityEngine;

namespace Map
{
  public class CameraController : MonoBehaviour
  {
    public Camera mainCamera => FindObjectOfType<Camera>();
    
    public CinemachineVirtualCamera virtualCamera{ get; private set; }

    public CinemachineConfiner2D confiner2D{ get; private set; }

    public CinemachineFollowZoom followZoom{ get; private set; }
    
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
      virtualCamera.m_LookAt = target;
    }

    public void SetZoom(float value)
    {
      // followZoom.m_Width = value;
      virtualCamera.m_Lens.OrthographicSize = value;
    }
  }
}
