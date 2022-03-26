using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController instance;
    [SerializeField] private Texture2D textureDefault;
    [SerializeField] private Texture2D textureOnClick;
    [SerializeField] private Vector2 hotspot;
    [SerializeField] private CursorMode cursorMode;

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

    void ResetTrigger()
    {
        Cursor.SetCursor(textureDefault,hotspot,cursorMode);
    }

    public void CursorClick()
    {
        Cursor.SetCursor(textureOnClick,hotspot,cursorMode);
        Invoke("ResetTrigger", 0.2f);
    }
}
