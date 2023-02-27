using UnityEngine;

/// <summary>
/// чисто тестовый скрипт чтобы протестить поведение пиксельной камеры
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class test : MonoBehaviour
{
    public Rigidbody2D testBody;


    // Start is called before the first frame update
    void Start()
    {
        testBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement;

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        testBody.velocity = movement * 5;
    }
}
