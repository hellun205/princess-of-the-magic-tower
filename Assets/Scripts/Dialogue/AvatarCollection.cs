using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Dialogue
{
  public class AvatarCollection : MonoBehaviour
  {
    [SerializedDictionary("key", "sprite")]
    public SerializedDictionary<string, Sprite> player;

    [SerializedDictionary("name", "sprite")]
    public SerializedDictionary<string, SerializedDictionary<string, Sprite>> npc;

    /// <summary>
    /// Get specific sprite of player
    /// </summary>
    /// <param name="type">Sprite type</param>
    /// <returns>Specific sprite</returns>
    /// <exception cref="Exception">Invalid sprite type</exception>
    public Sprite GetPlayerSprite(string type)
    {
      if (!player.ContainsKey(type))
        throw new Exception($"doesn't exist player avatar key: {type}");

      return player[type];
    }

    /// <summary>
    /// Get specific sprite of npc
    /// </summary>
    /// <param name="npcName">Npc name</param>
    /// <param name="type">Sprite type</param>
    /// <returns>Specific sprite</returns>
    /// <exception cref="Exception">Invalid npc name</exception>
    /// <exception cref="Exception">Invalid sprite type</exception>
    public Sprite GetNpcSprite(string npcName, string type)
    {
      if (!npc.ContainsKey(npcName))
        throw new Exception($"doesn't exist npc name: {npcName}");

      if (!npc[npcName].ContainsKey(type))
        throw new Exception($"doesn't exist npc avatar key: {type}");

      return npc[npcName][type];
    }

    /// <summary>
    /// <para>Get specific sprite of character with query</para>
    /// <para>Syntax: [character].[type]</para>
    /// Example)
    /// <code>
    /// player.normal
    /// test.angry
    /// </code>
    /// </summary>
    /// <param name="query">Query of avatar</param>
    /// <returns>Specific sprite</returns>
    /// <exception cref="Exception">Invalid npc name</exception>
    /// <exception cref="Exception">Invalid sprite type</exception>
    public Sprite GetSprite(string query)
    {
      var split = query.Split('.');

      if (split[0] == "player")
        return GetPlayerSprite(split[1]);
      else
        return GetNpcSprite(split[0], split[1]);
    }
  }
}
