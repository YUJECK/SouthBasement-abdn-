using SouthBasement.Dialogues;
using UnityEngine;

namespace cherrydev
{
    [System.Serializable]
    public class Sentence : DialogueNode
    {
        public DialogueNode ParentNode;
        public DialogueNode ChildNode;
        
        [field: SerializeField] public string CharacterName { get; set; }
        [field: SerializeField, TextArea(2, 10)] public string Text { get; set; }
        [field: SerializeField] public Sprite CharacterSprite { get; set; }

        public Sentence(string characterName, string text)
        {
            CharacterSprite = null;
            CharacterName = characterName;
            Text = text;
        }
    }
}