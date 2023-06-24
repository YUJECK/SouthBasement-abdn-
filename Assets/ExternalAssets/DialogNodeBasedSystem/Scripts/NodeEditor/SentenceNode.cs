using SouthBasement.Dialogues;
using UnityEditor;
using UnityEngine;

namespace cherrydev
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Nodes/Sentence Node", fileName = "New Sentence Node")]
    public class SentenceNode : Node
    {
        public override DialogueNode DialogueNode()
        {
            return sentence;
        }

        [SerializeField] private Sentence sentence;

        [Space(10)]
        public Node parentNode;

        public Node childNode;

        private const float lableFieldSpace = 40f;
        private const float textFieldWidth = 150f;

        public Sentence GetSentence() => sentence;
        public string GetSentenceCharacterName() => sentence.CharacterName;
        public string GetSentenceText() => sentence.Text;
        public Sprite GetCharacterSprite() => sentence.CharacterSprite;

#if UNITY_EDITOR

        /// <summary>
        /// Draw Sentence Node method
        /// </summary>
        /// <param name="nodeStyle"></param>
        /// <param name="lableStyle"></param>
        public override void Draw(GUIStyle nodeStyle, GUIStyle lableStyle)
        {
            base.Draw(nodeStyle, lableStyle);

            GUILayout.BeginArea(rect, nodeStyle);

            EditorGUILayout.LabelField("Sentence Node", lableStyle);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Name ", GUILayout.Width(lableFieldSpace));
            sentence.CharacterName = EditorGUILayout.TextField(sentence.CharacterName, GUILayout.Width(textFieldWidth));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Text ", GUILayout.Width(lableFieldSpace));
            sentence.Text = EditorGUILayout.TextField(sentence.Text, GUILayout.Width(textFieldWidth), GUILayout.ExpandHeight(true));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Sprite ", GUILayout.Width(lableFieldSpace));
            sentence.CharacterSprite = (Sprite)EditorGUILayout.ObjectField(sentence.CharacterSprite,
                typeof(Sprite), false, GUILayout.Width(textFieldWidth));
            EditorGUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        /// <summary>
        /// Adding nodeToAdd Node to the childNode field
        /// </summary>
        /// <param name="nodeToAdd"></param>
        /// <returns></returns>
        public override bool AddToChildConnectedNode(Node nodeToAdd)
        {
            SentenceNode sentenceNodeToAdd;

            if (nodeToAdd.GetType() == typeof(SentenceNode))
            {
                nodeToAdd = (SentenceNode)nodeToAdd;

                if (nodeToAdd == this)
                {
                    return false;
                }
            }

            if (nodeToAdd.GetType() == typeof(SentenceNode))
            {
                sentenceNodeToAdd = (SentenceNode)nodeToAdd;

                if (sentenceNodeToAdd != null && sentenceNodeToAdd.childNode == this)
                {
                    return false;
                }
            }

            childNode = nodeToAdd;
            return true;
        }

        /// <summary>
        /// Adding nodeToAdd Node to the parentNode field
        /// </summary>
        /// <param name="nodeToAdd"></param>
        /// <returns></returns>
        public override bool AddToParentConnectedNode(Node nodeToAdd)
        {
            SentenceNode sentenceNodeToAdd;

            if (nodeToAdd.GetType() == typeof(SentenceNode))
            {
                nodeToAdd = (SentenceNode)nodeToAdd;

                if (nodeToAdd == this)
                {
                    return false;
                }
            }

            parentNode = nodeToAdd;

            if (nodeToAdd.GetType() == typeof(SentenceNode))
            {
                sentenceNodeToAdd = (SentenceNode)nodeToAdd;

                if (sentenceNodeToAdd.childNode == this)
                {
                    return true;
                }
                else
                {
                    parentNode = null;
                }
            }

            return true;
        }

#endif
    }
}