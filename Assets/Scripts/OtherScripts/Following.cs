using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    public Transform ownObject;
    public Transform target;

    private void Update()
    {
        ownObject.position = target.position;
    }
}
