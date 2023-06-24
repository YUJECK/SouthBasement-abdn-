using System;
using SouthBasement.Dialogues;
using UnityEngine;

namespace cherrydev
{
    [Serializable]
    public class Answer : DialogueNode
    {
        [SerializeField, TextArea(2, 10)] public string[] Answers = new string[4];
        public Sentence[] ChildSentences = new Sentence[4];
    }
}