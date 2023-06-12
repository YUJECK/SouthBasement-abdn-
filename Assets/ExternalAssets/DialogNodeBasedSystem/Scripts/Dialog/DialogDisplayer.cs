using DG.Tweening;
using UnityEngine;

namespace cherrydev
{
    public class DialogDisplayer : MonoBehaviour
    {
        [SerializeField] private SentencePanel dialogSentencePanel;
        [SerializeField] private AnswerPanel dialogAnswerPanel;
        [SerializeField] private DialogBehaviour dialogBehaviour;

        private void OnEnable()
        {
            dialogBehaviour.AddListenerToOnDialogFinished(DisableDialogPanel);
            dialogBehaviour.AddListenerToOnDialogStarted(EnableDialogPanel);

            DialogBehaviour.OnAnswerButtonSetUp += SetUpAnswerButtonsClickEvent;

            DialogBehaviour.OnDialogSentenceEnd += dialogSentencePanel.ResetDialogText;

            DialogBehaviour.OnDialogTextCharWrote += dialogSentencePanel.AddCharToDialogText;

            DialogBehaviour.OnSentenceNodeActive += EnableDialogSentencePanel;
            DialogBehaviour.OnSentenceNodeActive += DisableDialogAnswerPanel;
            DialogBehaviour.OnSentenceNodeActiveWithParameter += dialogSentencePanel.AssignDialogNameTextAndSprite;

            DialogBehaviour.OnAnswerNodeActive += EnableDialogAnswerPanel;
            DialogBehaviour.OnAnswerNodeActive += DisableDialogSentencePanel;

            DialogBehaviour.OnAnswerNodeActiveWithParameter += dialogAnswerPanel.EnableCertainAmountOfButtons;

            DialogBehaviour.OnAnswerNodeSetUp += SetUpAnswerDialogPanel;
        }
        
        private void OnDisable()
        {
            DialogBehaviour.OnAnswerButtonSetUp -= SetUpAnswerButtonsClickEvent;

            DialogBehaviour.OnDialogSentenceEnd -= dialogSentencePanel.ResetDialogText;

            DialogBehaviour.OnDialogTextCharWrote -= dialogSentencePanel.AddCharToDialogText;

            DialogBehaviour.OnSentenceNodeActive -= EnableDialogSentencePanel;
            DialogBehaviour.OnSentenceNodeActive -= DisableDialogAnswerPanel;

            DialogBehaviour.OnSentenceNodeActiveWithParameter -= dialogSentencePanel.AssignDialogNameTextAndSprite;

            DialogBehaviour.OnAnswerNodeActive -= EnableDialogAnswerPanel;
            DialogBehaviour.OnAnswerNodeActive -= DisableDialogSentencePanel;

            DialogBehaviour.OnAnswerNodeActiveWithParameter -= dialogAnswerPanel.EnableCertainAmountOfButtons;

            DialogBehaviour.OnAnswerNodeSetUp -= SetUpAnswerDialogPanel;
        }

        private void EnableDialogPanel() => transform.DOMoveY(200f, 1f);
        private void DisableDialogPanel() => transform.DOMoveY(-400f, 1f);

        private void EnableDialogAnswerPanel()
        {
            ActiveGameObject(dialogAnswerPanel.gameObject, true);
            dialogAnswerPanel.DisalbleAllButtons();
        }

        private void DisableDialogAnswerPanel() => ActiveGameObject(dialogAnswerPanel.gameObject, false);

        private void EnableDialogSentencePanel() => ActiveGameObject(dialogSentencePanel.gameObject, true);

        private void DisableDialogSentencePanel() => ActiveGameObject(dialogSentencePanel.gameObject, false);

        private void ActiveGameObject(GameObject gameObject, bool isActive)
        {
            if (gameObject == null)
            {
                Debug.LogWarning("Game object is null");
                return;
            }

            gameObject.SetActive(isActive);
        }

        private void SetUpAnswerButtonsClickEvent(int index, AnswerNode answerNode)
        {
            dialogAnswerPanel.GetButtonByIndex(index).onClick.AddListener(() =>
            {
                dialogBehaviour.SetCurrentNodeAndHandleDialogGraph(answerNode.childSentenceNodes[index]);
            });
        }

        private void SetUpAnswerDialogPanel(int index, string answerText) => dialogAnswerPanel.GetButtonTextByIndex(index).text = answerText;
    }
}