using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRoom : MonoBehaviour
{
    public GameObject CloseWalls;
    public void Close()
    {
        CloseWalls.gameObject.SetActive(true);
    }
    public void Open()
    {
        CloseWalls.gameObject.SetActive(false);
    }
}
