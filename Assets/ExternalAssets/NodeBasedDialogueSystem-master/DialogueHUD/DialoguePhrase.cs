namespace Subtegral.DialogueSystem.Runtime
{
    public sealed class DialoguePhrase
    {
        public string TableName;
        public readonly string Text;
        public readonly DialogueChoice[] DialogueChoices;

        public DialoguePhrase(string tableName, string text, DialogueChoice[] dialogueChoices)
        {
            Text = text;
            TableName = tableName;
            DialogueChoices = dialogueChoices;
        }
    }
}