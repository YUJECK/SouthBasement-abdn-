using System;
using System.Collections;
using System.Collections.Generic;
using Subtegral.DialogueSystem.DataContainers;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Subtegral.DialogueSystem.Editor
{
    public class DialogueNode : Node
    {
        public string DialogueText;
        public string GUID;
        public bool EntyPoint = false;
    }
}