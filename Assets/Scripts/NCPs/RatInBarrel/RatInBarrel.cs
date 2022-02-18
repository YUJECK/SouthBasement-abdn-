using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatInBarrel : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private EButton e;

    [SerializeField]
    private GameObject ChillyPepper;

    [SerializeField]
    private bool isItemGave = false;

    [SerializeField]
    private bool isShowRat;

    [SerializeField]
    private bool isOnTrigger;

    [SerializeField]
    private bool isShowing = false;

    [SerializeField]
    private float time;

    [SerializeField]
    private float showTime;


    private void Update()
    {
        if (isOnTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ShowRat();
                showTime = Time.time + 2.2f;
                isShowing = true;
            }
        }

        if (isShowRat)
            e.Disable();

        //�������� ������� ��������
        if(Time.time >= showTime&isShowing)
        {
            anim.SetBool("GiveItem", true);
            time = Time.time + 1f;
            isShowing = false;
        }
        if(anim.GetBool("GiveItem"))
        {
            if (Time.time >= time)
                anim.SetBool("GiveItem", false);      
        }
    }
    public void ShowRat()
    {
        anim.SetTrigger("ShowRat");
        isShowRat = true;
    }
    public void ShowItem()
    {
        if(!isItemGave)
        {
            ChillyPepper.SetActive(true);
            isItemGave = true;
        }
    }
    public void DisableItemAnim()
    {
        Debug.Log("End");
        anim.SetBool("GiveItem", false);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isOnTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetTrigger("Don'tShowRat");
        anim.ResetTrigger("ShowRat");
        isShowRat = false;
        isOnTrigger = false;
    }
}
