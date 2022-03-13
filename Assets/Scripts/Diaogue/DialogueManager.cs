using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Dialogue dialogue; // Фразы 
    public DialogueTrigger Trigger;
    public InteractivePanel panel;
    public bool isStart = true;
    public Text NameText;
    public Text DialogueText;
    public int SentencesSays = 0;
    public int ExtraSentences = 0;

    private void Start()
    {
        ExtraSentences = 0;
    }
    public void StartDialogue(Dialogue NewDialogue)
    {
        SentencesSays = 0;
        dialogue = NewDialogue;
        dialogue.DialogueCloud.enabled = true;
        NameText.text = dialogue.NPC_name;
        DialogueText.text = dialogue.sentences[0];

        if(dialogue.InteractivePanel != null)
            ActiveInteractivePanel();
        if(ExtraSentences >= 1)
            DisplayExtraSentences();
        else
            DisplayNextSentence();
        isStart = false;
    }
    public void DisplayNextSentence()
    {        
        if (SentencesSays == dialogue.sentences.Length)
            DialogueEnd();
        DialogueText.text = dialogue.sentences[SentencesSays];
        SentencesSays++;
    }
    public void DialogueEnd()
    {
        Trigger.dialogue.DialogueCloud.enabled = false;
        
        if(Trigger.dialogue.InteractivePanel != null)
            Trigger.dialogue.InteractivePanel.enabled = false;
        SentencesSays = 0;
        isStart = true;
    }
    public void DisplayText(string text)
    {
        Trigger.dialogue.DialogueCloud.enabled = true;
        DialogueText.text = text;
    }
    public void DisplayExtraSentences()
    {
        if (ExtraSentences == 0)
        { DialogueEnd(); }
        ExtraSentences--;
        DialogueText.text = dialogue.extraSentences[ExtraSentences];
    }
    public void ActiveInteractivePanel()
    {
        Trigger.dialogue.InteractivePanel.enabled = true;
        panel.ActivePanel();
    }
}
