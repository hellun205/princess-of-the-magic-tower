using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Dialogue
{
  public class DialogueManager : MonoBehaviour
  {
    private Binder binder;
    private GameObject panel;

    private TextMeshProUGUI contentText => binder.Get<TextMeshProUGUI>("content_text");
    private TextMeshProUGUI talkerName => binder.Get<TextMeshProUGUI>("talker_text");

    private Script currentScript;
    private int currentIndex;
    private bool canNext;

    [NonSerialized]
    public bool isShowing;

    private void Awake()
    {
      panel = GameManager.ManagedObject.Find("$dialogue_panel");
      binder = new Binder(panel).Bind('*');
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

    public void SetTalker(string name)
      => talkerName.text = name;

    private void StartDialogue(IDialogueContent[] dialogues, Action callback = null)
      => StartCoroutine(DialogueRoutine(dialogues, callback));

    private IEnumerator DialogueRoutine(IDialogueContent[] dialogues, Action callback = null)
    {
      foreach (var dialogue in dialogues)
        yield return StartCoroutine(dialogue.Active());

      callback?.Invoke();
    }

    public void StartScript(string scriptKey, Action callback = null)
      => StartScript(ScriptDatabase.scripts[scriptKey], callback);

    public void StartScript(Script script, Action callback = null)
    {
      if (isShowing) return;
      isShowing = true;
      currentScript = script;
      currentIndex = 0;
      panel.SetActive(true);
      ShowScript(currentIndex);
    }

    public void OnClick()
    {
      if (!canNext) return;

      if (currentScript.data.Count - 1 > currentIndex)
        ShowScript(++currentIndex);
      else
        Exit();
    }

    public void Exit()
    {
      isShowing = false;
      panel.SetActive(false);
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

    [ContextMenu("Test Dialogue")]
    public void TestDialogue()
    {
      StartScript("test");
    }
  }
}
