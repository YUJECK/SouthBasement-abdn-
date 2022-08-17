using UnityEngine;

public class SetCanvasCamera : MonoBehaviour
{
    void Start() 
    {
        if (GetComponent<Canvas>().worldCamera != null) 
            GetComponent<Canvas>().worldCamera = Camera.main; 
    }
}
