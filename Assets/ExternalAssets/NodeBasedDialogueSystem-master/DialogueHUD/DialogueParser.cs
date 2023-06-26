using System.Collections.Generic;
using System.Linq;
using Subtegral.DialogueSystem.DataContainers;

namespace Subtegral.DialogueSystem.Runtime
{
    public sealed class DialogueParser 
    {
        public DialoguePhrase Get(DialogueContainer dialogue, string narrativeDataGUID)
        {
            var text = dialogue.DialogueNodeData.Find(x => x.NodeGUID == narrativeDataGUID).DialogueText;
            text = ProcessProperties(dialogue, text);
            var choices = GetChoices(dialogue, narrativeDataGUID);

            return new DialoguePhrase(text, choices);
        }

        public DialogueChoice[] GetChoices(DialogueContainer dialogue, string narrativeDataGUID)
        {
            var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID);
            var choicesResult = new List<DialogueChoice>();
            
            foreach (var choice in choices)
                choicesResult.Add(new DialogueChoice(ProcessProperties(dialogue, choice.PortName), choice.TargetNodeGUID));
            
            return choicesResult.ToArray();
        }

        public string ProcessProperties(DialogueContainer dialogue, string text)
        {
            foreach (var exposedProperty in dialogue.ExposedProperties)
            {
                text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);
            }
            return text;
        }
    }
}