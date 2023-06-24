using System;
using UnityEngine;
using cherrydev;
using SouthBasement.Interactions;

public class TestDialogStarter : MonoBehaviour, IInteractive
{
    private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogGraph;

    private void Awake()
    {
        dialogBehaviour = FindObjectOfType<DialogBehaviour>();
    }

    public void Detect()
    {
        
    }

    public void Interact()
    {
        dialogBehaviour.StartDialogue(dialogGraph.DialogueGraph);        
    }

    public void DetectionReleased()
    {
        
    }
}