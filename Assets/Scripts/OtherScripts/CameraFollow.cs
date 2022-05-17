using UnityEngine;

using static RimuruDev.Helpers.Tag;

public class CameraFollow : MonoBehaviour
{
    public float standartValaue = 6f;
    public Rigidbody2D Camera;
    public Rigidbody2D RatCharacter;
    
    private Transform AnotherTrigger;
    private bool isAnotherTrigger;
    
    public static CameraFollow instance;

    private void Awake()
    {
        //RatCharacter = RatCharacter.GetComponent<Rigidbody2D>();
        RatCharacter = GameObject.FindGameObjectWithTag(Player).GetComponent<Rigidbody2D>();
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

    void LateUpdate()
    {
        if(AnotherTrigger == null)
            isAnotherTrigger = false;
        
        if(!isAnotherTrigger)
            Camera.position = RatCharacter.position;

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
