using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EButton : MonoBehaviour
{
    public GameObject e;
    public bool ShowE = false;
    public bool isActive = true;
    private void Update()
    {
        if (isActive)
        {
            if (ShowE) e.SetActive(true);
            else e.SetActive(false);
        }
        else e.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Active();
    }
    private void OnTriggerExit2D(Collider2D collision) {Disable();}
    public void Active(){ShowE = true;}
    public void Disable(){ShowE = false;}
}
