using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GenerationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DiContainer diContainer = new DiContainer();


        diContainer.In();



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
