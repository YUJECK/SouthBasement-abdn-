using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public InteractivePanel panel;
    public bool DisableE;
    public EButton e;
    public DialogueManager Manager;
    public bool isOnTrigger = false;
    public bool NotStandartDialog = false;
    private void Start()
    {
        dialogue.DialogueCloud.enabled = false;
        if(dialogue.InteractivePanel != null)
        {
            dialogue.InteractivePanel.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            isOnTrigger = true;
        }
    }
    void OnTriggerExit2D()
    {
        isOnTrigger = false;
        Manager.DialogueEnd();
    }

    private void Update()
    {
        if(!NotStandartDialog)
        {
            if (isOnTrigger == true)
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    TriggerDialogue();
                    if (DisableE)
                        e.Disable();
                }
            }
        }
    }
    public void TriggerDialogue()
    {
        if(Manager.isStart)
        {
            Manager.StartDialogue(dialogue);
            Manager.isStart = false;
            if(dialogue.InteractivePanel!=null)
            {
                panel.ActivePanel();
            }
        }
        else
        {
            if (Manager.ExtraSentences != 0)
                Manager.DisplayExtraSentences();
            else
                Manager.DisplayNextSentence();
        }
    }
}
