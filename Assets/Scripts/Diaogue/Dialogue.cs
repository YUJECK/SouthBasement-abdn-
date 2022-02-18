using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string NPC_name;
    [TextArea(3,10)]
    public string[] sentences;
    [TextArea(3, 10)]
    public string[] extraSentences;
    public Canvas DialogueCloud;
    public Canvas InteractivePanel;
}