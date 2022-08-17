using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    public Transform target;

    private void FixedUpdate()
    {
        if (new Vector3(transform.position.x, transform.position.y, 0f) != new Vector3(target.position.x, target.position.y, 0f))
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
