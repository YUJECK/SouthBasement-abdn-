using System;
using UnityEngine;

namespace Subtegral.DialogueSystem.Runtime
{
    [Serializable]
    public sealed class DialogueChoice
    {
        [field: SerializeField] public string TableName { get; private set; }
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public string Target { get; private set; }

        public DialogueChoice(string tableName, string text, string target)
        {
            Text = text;
            Target = target;
            TableName = tableName;
        }
    }
}