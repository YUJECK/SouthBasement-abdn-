using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
{
    public RatInBarrel rat;

    private void Start()
    {
        rat = FindObjectOfType<RatInBarrel>();
    }
    void ShowItem()
    {
        rat.ShowItem();
    }
    void DisableItemAnim()
    {
        rat.DisableItemAnim();
    }
}
