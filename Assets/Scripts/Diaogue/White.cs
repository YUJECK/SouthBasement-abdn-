using UnityEngine;
using System.Collections;

public class White : MonoBehaviour
{
    public Animator anim;

    void OnTriggerEnter2D()
    {
        anim.SetBool("White", true);       
    }
    void OnTriggerExit2D()
    {
        anim.SetBool("White", false);
    }
}