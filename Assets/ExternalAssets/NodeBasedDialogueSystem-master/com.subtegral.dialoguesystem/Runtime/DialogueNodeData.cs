using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Subtegral.DialogueSystem.DataContainers
{
    [Serializable]
    public class DialogueNodeData
    {
        public string NodeGUID;
        [FormerlySerializedAs("DialogueText")] public string TableStringReference;
        public Vector2 Position;
    }
}