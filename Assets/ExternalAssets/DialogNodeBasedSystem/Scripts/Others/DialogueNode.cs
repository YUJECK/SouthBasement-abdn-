using System;
using System.Collections.Generic;

namespace SouthBasement.Dialogues
{
    [Serializable]
    public class DialogueNode
    {
        public DialogueNode ParentNode;
        public List<DialogueNode> ConnectedNodes;

        public void Init(List<DialogueNode> nodes)
        {
            ConnectedNodes = nodes;
        }
    }
}