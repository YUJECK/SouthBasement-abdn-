using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Mode
{
    Prefabs,
    Sprites,
}
public class PrefabRandomizer : MonoBehaviour
{
    public Mode mode = Mode.Prefabs;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int layer;

    private void Start()
    {
        if(mode == Mode.Prefabs)
        {
            int prefabIndex = Random.Range(0, prefabs.Length);
            Instantiate(prefabs[prefabIndex], gameObject.transform.position, Quaternion.identity, transform); 
        }
        else if(mode == Mode.Sprites)
        {
            int spriteIndex = Random.Range(0, sprites.Length);
            gameObject.AddComponent<SpriteRenderer>().sprite = sprites[spriteIndex];
        }
    }
}
