using System;
using System.Collections.Generic;

namespace SouthBasement.Dialogues
{
    [Serializable]
    public sealed class DialogueGraph
    {
        public List<DialogueNode> DialogueNodes = new();
    }
}