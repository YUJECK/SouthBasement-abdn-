using cherrydev;

namespace SouthBasement.Dialogues
{
    public interface IDialogueBehaviour
    {
        public void StartDialogue(DialogueGraph nodeGraph);
        public void StopDialogue();

        public void DisplaySentence(Sentence sentenceNode);
    }
}