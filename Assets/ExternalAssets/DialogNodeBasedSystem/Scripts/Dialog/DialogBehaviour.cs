using System;
using System.Collections;
using SouthBasement.Dialogues;
using UnityEngine;
using UnityEngine.Events;

namespace cherrydev
{
    public class DialogBehaviour : MonoBehaviour, IDialogueBehaviour
    {
        [SerializeField] private float dialogCharDelay;
        [SerializeField] private KeyCode nextSentenceKeyCode;

        [SerializeField] private UnityEvent onDialogStart;
        [SerializeField] private UnityEvent onDialogFinished;

        private DialogueGraph currentNodeGraph;
        private DialogueNode currentNode;

        public static event Action OnSentenceNodeActive;

        public static event Action OnDialogSentenceEnd;

        public static event Action<string, Sprite> OnSentenceNodeActiveWithParameter;

        public static event Action OnAnswerNodeActive;

        public static event Action<int, Answer> OnAnswerButtonSetUp;

        public static event Action<int> OnAnswerNodeActiveWithParameter;

        public static event Action<int, string> OnAnswerNodeSetUp;

        public static event Action<char> OnDialogTextCharWrote;

        public void AddListenerToOnDialogStarted(UnityAction action) => onDialogStart.AddListener(action);
        public void AddListenerToOnDialogFinished(UnityAction action) => onDialogFinished.AddListener(action);

        public void StartDialogue(DialogueGraph dialogNodeGraph)
        {
            if (dialogNodeGraph == null)
            {
                Debug.LogWarning("Dialog Graph's node list is empty");
                return;
            }

            onDialogStart?.Invoke();
            
            currentNodeGraph = dialogNodeGraph;
            currentNode = currentNodeGraph.DialogueNodes[0];

            HandleDialogGraphCurrentNode(currentNode);
        }

        public void StopDialogue()
        {
            onDialogFinished?.Invoke();
        }

        private void HandleDialogGraphCurrentNode(DialogueNode currentNode)
        {
            StopAllCoroutines();

            if (currentNode is Sentence sentenceNode)
            {
                DisplaySentence(sentenceNode);
            }
            else if (currentNode is Answer answerNode)
            {
                DisplayAnswerNode(answerNode);
            }
        }

        private void DisplayAnswerNode(Answer currentNode)
        {
            Answer answerNode = currentNode;
            int amountOfActiveButtons = 0;

            OnAnswerNodeActive?.Invoke();

            for (int i = 0; i < answerNode.ChildSentences.Length; i++)
            {
                if (answerNode.ChildSentences[i] != null)
                {
                    OnAnswerNodeSetUp?.Invoke(i, answerNode.Answers[i]);
                    OnAnswerButtonSetUp?.Invoke(i, answerNode);

                    amountOfActiveButtons++;
                }
                else
                {
                    break;
                }
            }

            if (amountOfActiveButtons == 0)
            {
                onDialogFinished?.Invoke();
                return;
            }

            OnAnswerNodeActiveWithParameter?.Invoke(amountOfActiveButtons);
        }

        public void DisplaySentence(Sentence currentNode)
        {
            var sentenceNode = currentNode;

            OnSentenceNodeActive?.Invoke();
            OnSentenceNodeActiveWithParameter?.Invoke(sentenceNode.CharacterName,
                sentenceNode.CharacterSprite);

            WriteDialogText(sentenceNode.Text);
        }

        public void SetCurrentNodeAndHandleDialogGraph(DialogueNode node)
        {
            currentNode = node;
            HandleDialogGraphCurrentNode(this.currentNode);
        }

        private void WriteDialogText(string text) => StartCoroutine(WriteDialogTextRoutine(text));

        private IEnumerator WriteDialogTextRoutine(string text)
        {
            foreach (char textChar in text)
            {
                yield return new WaitForSeconds(dialogCharDelay);
                OnDialogTextCharWrote?.Invoke(textChar);
            }

            yield return new WaitUntil(() => Input.GetKeyDown(nextSentenceKeyCode));

            OnDialogSentenceEnd?.Invoke();
            CheckForDialogNextNode();
        }

        private void CheckForDialogNextNode()
        {
            if (currentNode == null)
            {
                onDialogFinished?.Invoke();
                return;
            }
                
            if (currentNode.GetType() == typeof(Sentence))
            {
                Sentence sentenceNode = (Sentence)currentNode;

                if (sentenceNode.ChildNode != null)
                {
                    currentNode = sentenceNode.ChildNode;
                    HandleDialogGraphCurrentNode(currentNode);
                }
                else
                {
                    onDialogFinished?.Invoke();
                }
            }
        }
    }
}