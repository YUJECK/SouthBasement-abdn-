using System;
using Subtegral.DialogueSystem.DataContainers;

namespace SouthBasement.Dialogues
{
    public interface IDialogueService
    {
        bool CurrentlyTalk { get; }

        void StartDialogue(DialogueContainer dialogueContainer, Action onStopped);
        void StopDialogue();
    }
}