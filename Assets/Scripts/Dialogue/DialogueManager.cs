using System;
using System.Collections;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Touch = UI.Touch;

namespace Dialogue
{
  public class DialogueManager : MonoBehaviour
  {
    private Binder binder;
    private GameObject panel;

    private TextMeshProUGUI contentText => binder.Get<TextMeshProUGUI>("content_text");

    private Script currentScript;
    private int currentIndex;
    private bool canNext;

    [NonSerialized]
    public bool isShowing;

    private void Awake()
    {
      panel = GameManager.ManagedObject.Get("dialogue_panel");
      binder = new Binder(panel).Bind('*');
      panel.GetComponent<Touch>().onTouch.AddListener(_ => OnClick());
      panel.SetActive(false);
    }

    public void AppendText(string text)
      => contentText.text += text;

    public void AppendText(char text)
      => contentText.text += text;

    public void SetText(string text)
      => contentText.text = text;

    public void Clear()
      => contentText.text = string.Empty;

    private void StartDialogue(IDialogueContent[] dialogues, Action callback = null)
      => StartCoroutine(DialogueRoutine(dialogues, callback));

    private IEnumerator DialogueRoutine(IDialogueContent[] dialogues, Action callback = null)
    {
      foreach (var dialogue in dialogues)
        yield return StartCoroutine(dialogue.Active());

      callback?.Invoke();
    }

    private void ShowScript(int index)
    {
      var script = currentScript[index];
      var isLeft = script.direction == "left";
      var avatar = GameManager.Avatars.GetSprite(script.avatar);
      Clear();
      canNext = false;

      binder.Get<TextMeshProUGUI>($"talker_name_{script.direction}").text = script.name;
      binder.Get<Image>($"talker_avatar_{script.direction}").sprite = avatar;

      binder["talker_avatar_left"].SetActive(isLeft);
      binder["talker_avatar_right"].SetActive(!isLeft);
      binder["talker_name_panel_left"].SetActive(isLeft);
      binder["talker_name_panel_right"].SetActive(!isLeft);

      StartDialogue(script.dialogue, () => canNext = true);
    }

    /// <summary>
    /// <inheritdoc cref="StartScript(Script,System.Action)"/>
    /// </summary>
    /// <param name="scriptKey">Key in Script database</param>
    /// <param name="callback">Function of finished</param>
    public void StartScript(string scriptKey, Action callback = null)
      => StartScript(ScriptDatabase.scripts[scriptKey], callback);

    /// <summary>
    /// 해당 스크립트를 실행합니다.
    /// </summary>
    /// <param name="script">Script data</param>
    /// <param name="callback">Function of fisished</param>
    public void StartScript(Script script, Action callback = null)
    {
      if (isShowing) return;
      isShowing = true;
      currentScript = script;
      currentIndex = 0;
      panel.SetActive(true);
      ShowScript(currentIndex);
    }

    /// <summary>
    /// On Dialogue Panel Click
    /// </summary>
    public void OnClick()
    {
      if (!canNext) return;

      if (currentScript.data.Count - 1 > currentIndex)
        ShowScript(++currentIndex);
      else
        Exit();
    }

    /// <summary>
    /// 대화를 종료합니다.
    /// </summary>
    public void Exit()
    {
      isShowing = false;
      panel.SetActive(false);
    }

#if UNITY_EDITOR
    [ContextMenu("Test Dialogue")]
    public void Test()
    {
      StartScript("test");
    }
#endif
  }
}