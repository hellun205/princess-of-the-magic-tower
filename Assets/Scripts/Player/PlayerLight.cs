using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Player
{
  public class PlayerLight : MonoBehaviour
  {
    [SerializedDictionary("Key", "Light Object")]
    public SerializedDictionary<string, Light2D> lights;

    public string current => lights.Values.Single(lightObj => lightObj.gameObject.activeSelf).name;

    public void SetLight(string key)
    {
      if (!lights.ContainsKey(key))
        Debug.LogError($"doesn't exist light key: ${key}");
      
      foreach (var (lightKey, lightObj) in lights)
        lightObj.gameObject.SetActive(lightKey == key);
    }

    public void SetDefault()
      => SetLight("default");
  }
}
