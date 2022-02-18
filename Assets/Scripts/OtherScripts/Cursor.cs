using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    Animator anim; 
    Vector3 tmp;
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
    void Update()
    {
        tmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tmp.z = 0f;
        transform.position = tmp;

        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("OnClick");
            Invoke("ResetTrigger", 0.2f);
        }
    }

    void ResetTrigger()
    {
        anim.ResetTrigger("OnClick");
    }
}
