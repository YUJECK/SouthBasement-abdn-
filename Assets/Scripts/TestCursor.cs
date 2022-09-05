using UnityEngine;

public class TestCursor : MonoBehaviour
{
    public Camera camera;
    void Update()
    {
        transform.position = 
        new Vector3(camera.ScreenToWorldPoint(Input.mousePosition).x, 
        camera.ScreenToWorldPoint(Input.mousePosition).y, 0f);
    }
}