using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float standartValaue = 6f;
    public Rigidbody2D Camera;
    public Rigidbody2D Player;
    
    private Transform AnotherTrigger;
    private bool isAnotherTrigger;
    
    public static CameraFollow instance;

    private void Awake()
    {
        Player = Player.GetComponent<Rigidbody2D>();
        Camera = Camera.GetComponent<Rigidbody2D>();
    
        if(instance == null)
            instance = this;

        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    private void OnLevelWasLoaded(int level)
    {
        GetComponent<Camera>().orthographicSize = standartValaue;
    }

    void FixedUpdate()
    {
        if(AnotherTrigger == null)
            isAnotherTrigger = false;
        
        if(!isAnotherTrigger)
            Camera.position = Player.position;

        else if(AnotherTrigger != null)
            Camera.position = AnotherTrigger.position;
    }

    public void SetTrigger(Transform newTrigger)
    {
        AnotherTrigger = newTrigger;
        isAnotherTrigger = true;
    }
    public void ResetTrigger() 
    {
        AnotherTrigger = null;
        isAnotherTrigger = false;
    }
}
