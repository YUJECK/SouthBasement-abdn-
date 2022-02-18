using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Rigidbody2D Player;
    public Rigidbody2D Camera;
    void Start()
    {
        Camera = Camera.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Camera.position = Player.position;
    }
}
