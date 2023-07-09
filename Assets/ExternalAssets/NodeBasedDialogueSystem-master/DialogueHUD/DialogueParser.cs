using System.Collections.Generic;
using System.Linq;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

namespace Subtegral.DialogueSystem.Runtime
{
    public sealed class DialogueParser 
    {
        public DialoguePhrase Get(DialogueContainer dialogue, string narrativeDataGUID)
        {
            var tableNameReference = dialogue.DialogueNodeData.Find(x => x.NodeGUID == narrativeDataGUID).TableStringReference;
            tableNameReference = ProcessProperties(dialogue, tableNameReference);

            LocalizedString text = new(dialogue.TableName, tableNameReference);
            
            var choices = GetChoices(dialogue, narrativeDataGUID);

            return new DialoguePhrase(dialogue.TableName, text.GetLocalizedString(), choices);
        }

        public DialogueChoice[] GetChoices(DialogueContainer dialogue, string narrativeDataGUID)
        {
            var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID);
            var choicesResult = new List<DialogueChoice>();
            
            foreach (var choice in choices)
                choicesResult.Add(new DialogueChoice(dialogue.TableName, ProcessProperties(dialogue, choice.PortName), choice.TargetNodeGUID));
            
            return choicesResult.ToArray();
        }

        public string ProcessProperties(DialogueContainer dialogue, string text)
        {
            foreach (var exposedProperty in dialogue.ExposedProperties)
                text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);

            return text;
        }
    }
}