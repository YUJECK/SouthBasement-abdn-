using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabRandomizer : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    private void Start()
    {
        int prefabIndex = Random.Range(0,prefabs.Length);

        Instantiate(prefabs[prefabIndex],gameObject.transform.position, Quaternion.identity); 
    }
}
