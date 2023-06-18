using UnityEngine;
using cherrydev;
using SouthBasement.Interactions;

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