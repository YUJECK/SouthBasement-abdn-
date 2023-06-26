namespace Subtegral.DialogueSystem.Runtime
{
    public sealed class DialoguePhrase
    {
        public string Text;
        public DialogueChoice[] DialogueChoices;

        public DialoguePhrase(string text, DialogueChoice[] dialogueChoices)
        {
            Text = text;
            DialogueChoices = dialogueChoices;
        }
    }
}