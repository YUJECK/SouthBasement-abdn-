using UnityEngine;

public class SetCanvasCamera : MonoBehaviour
{
    void Start() => gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
}
