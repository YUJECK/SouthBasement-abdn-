using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Rigidbody2D RatCharacterData;
    public Rigidbody2D Camera;
    void Start()
    {
        Camera = Camera.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Camera.position = RatCharacterData.position;
    }
}
