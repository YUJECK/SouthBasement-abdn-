namespace Subtegral.DialogueSystem.Runtime
{
    public sealed class DialogueChoice
    {
        public string Text;
        public string Target;

        public DialogueChoice(string text, string target)
        {
            Text = text;
            Target = target;
        }
    }
}