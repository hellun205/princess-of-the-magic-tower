using System.Collections.Generic;

namespace Dialogue
{
  public static class ScriptDatabase
  {
    public static Dictionary<string, Script> scripts;

    static ScriptDatabase()
    {
      scripts = new()
      {
        {
          "test",
          new Script()
            .SetPlayerAvatar("player.normal")
            .SetOpponentName("테스트")
            .SetOpponentAvatar("test.normal")
            .Player
            (
              new DText(". . . ", 0.15f),
              new DWait(0.25f),
              new DText("하. . . "),
              new DWait(0.25f),
              new DText("저는 대사를 테스트 중인 공주에요. ")
            )
            .Player
            (
              new DText("ㅋㅋ "),
              new DWait(0.25f),
              new DText(". . . ", 0.15f),
              new DWait(0.25f),
              new DText("저는 상대방을 죽일겁니다. ")
            )
            .Opponent
            (
              new DText("저는 상대방입니다. "),
              new DWait(0.5f),
              new DText("하하 . . ")
            )
            .Opponent
            (
              new DText("저는 공주한테 죽을거에요"),
              new DText(". . . ", 0.4f),
              new DWait(0.3f),
              new DText("진짜 죽을거같음.")
            )
            .Player
            (
              new DStyleText("정 말 나 는 너 무 강 해 !", 0.2f).AddTag("size", "2em").AddTag("color", "red"),
              new DText("\n어떻게 생각하세요?")
            )
        }
      };
    }
  }
}