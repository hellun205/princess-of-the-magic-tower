using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Sound
{
  public string name;
  public AudioClip _clip;
}

public class AudioManager : MonoBehaviour
{
  public Sound[] sounds;

  public AudioSource sfxSource;


  public void PlaySfx(string name)
  {
    for (int i = 0; i < sounds.Length; i++)
    {
      if (sounds[i].name == name)
      {
        sfxSource.clip = sounds[i]._clip;
        sfxSource.PlayOneShot(sounds[i]._clip);
      }
    }
  }

  public void PlayKillSfx()
  {
    int random = Random.Range(1, 5);

    switch (random)
    {
      case 1:
        for (int i = 0; i < sounds.Length; i++)
        {
          if (sounds[i].name == "dash_hit1")
          {
            sfxSource.clip = sounds[i]._clip;
            sfxSource.PlayOneShot(sounds[i]._clip);
          }
        }
        break;
      case 2:
        for (int i = 0; i < sounds.Length; i++)
        {
          if (sounds[i].name == "dash_hit2")
          {
            sfxSource.clip = sounds[i]._clip;
            sfxSource.PlayOneShot(sounds[i]._clip);
          }
        }
        break;
      case 3:
        for (int i = 0; i < sounds.Length; i++)
        {
          if (sounds[i].name == "dash_hit3")
          {
            sfxSource.clip = sounds[i]._clip;
            sfxSource.PlayOneShot(sounds[i]._clip);
          }
        }
        break;
      case 4:
        for (int i = 0; i < sounds.Length; i++)
        {
          if (sounds[i].name == "dash_hit4")
          {
            sfxSource.clip = sounds[i]._clip;
            sfxSource.PlayOneShot(sounds[i]._clip);
          }
        }
        break;
    }
  }
}