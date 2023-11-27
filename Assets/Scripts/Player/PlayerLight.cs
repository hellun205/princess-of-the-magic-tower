using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Player
{
  public class PlayerLight : MonoBehaviour
  {
    [SerializedDictionary("Key", "Light Object")]

    public GameObject dayLight;
    public GameObject nightLight;
    
    public void SetLight(string key)
    {
        dayLight.SetActive(key == "default");
        nightLight.SetActive(key == "night");
    }

    public void SetDefault()
      => SetLight("default");
  }
}
