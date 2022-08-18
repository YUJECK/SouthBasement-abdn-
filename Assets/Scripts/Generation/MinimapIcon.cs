using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    [Header("Настройки отображения")]
    [SerializeField] protected string minimapLayer = "Blocks"; //Слой который читает миникарта
    [SerializeField] private GameObject[] objectsToChangeLayer; //Объекты которым надо поменять слой
    private bool layerChanged = false; //Был ли уже изменен слой

    //Активация
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !layerChanged)
        {
            foreach (GameObject nextObject in objectsToChangeLayer)
            {
                nextObject.layer = LayerMask.NameToLayer(minimapLayer);
                Transform[] children = nextObject.GetComponentsInChildren<Transform>();
                
                foreach(Transform nextChild in children)
                    nextChild.gameObject.layer = LayerMask.NameToLayer(minimapLayer);
                
                layerChanged = true;
            }
        }
    }
}
