using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private Animator anim; 
    private Vector3 tmp;
    public static Cursor instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        tmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tmp.z = 0f;
        transform.position = tmp;
    }

    void ResetTrigger()
    {
        anim.ResetTrigger("OnClick");
    }

    public void CursorClick()
    {
        anim.SetTrigger("OnClick");
        Invoke("ResetTrigger", 0.2f);
    }
}
