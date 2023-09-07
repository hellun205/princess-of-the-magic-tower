using System.Collections.Generic;

namespace Dialogue
{
  public class Script
  {
    public List<ScriptData> data = new();

    public ScriptData this[int index] => data[index];

    private string playerName = "공주";
    
    private string opponentName;

    private string playerAvatar;

    private string opponentAvatar;

    /// <summary>
    /// 플레이어의 이름을 설정합니다.
    /// </summary>
    /// <param name="name">표시 이름</param>
    public Script SetPlayerName(string name)
    {
      playerName = name;
      return this;
    }
    
    /// <summary>
    /// 상대방의 이름을 설정합니다.
    /// </summary>
    /// <param name="name">표시 이름</param>
    public Script SetOpponentName(string name)
    {
      opponentName = name;
      return this;
    }

    /// <summary>
    /// 상대방의 초상화를 설정합니다.
    /// </summary>
    /// <param name="query">Query</param>
    public Script SetOpponentAvatar(string query)
    {
      opponentAvatar = query;
      return this;
    }

    /// <summary>
    /// 플레이어의 초상화를 설정합니다.
    /// </summary>
    /// <param name="query">Query</param>
    public Script SetPlayerAvatar(string query)
    {
      playerAvatar = query;
      return this;
    }

    /// <summary>
    /// 다음 대화를 플레이어의 대사로 설정합니다.
    /// </summary>
    /// <param name="dialogue">대사</param>
    public Script Player(params IDialogueContent[] dialogue) 
    {
      data.Add(new ScriptData()
      {
        name = playerName,
        avatar = playerAvatar,
        direction = "left",
        dialogue = dialogue
      });
      
      return this;
    }
    
    /// <summary>
    /// 다음 대화를 상대방의 대사로 설정합니다.
    /// </summary>
    /// <param name="dialogue">대사</param>
    public Script Opponent(params IDialogueContent[] dialogue) 
    {
      data.Add(new ScriptData()
      {
        name = opponentName,
        avatar = opponentAvatar,
        direction = "right",
        dialogue = dialogue
      });
      
      return this;
    }
  }
}
