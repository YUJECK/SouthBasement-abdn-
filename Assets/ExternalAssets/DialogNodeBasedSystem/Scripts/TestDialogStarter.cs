using UnityEngine;
using cherrydev;
using TheRat.Interactions;

public class TestDialogStarter : MonoBehaviour, IInteractive
{
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogGraph;

    public void Detect()
    {
        
    }

    public void Interact()
    {
        dialogBehaviour.StartDialog(dialogGraph);        
    }

    public void DetectionReleased()
    {
        
    }
}