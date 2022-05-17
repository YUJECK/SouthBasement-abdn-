using RimuruDev.Mechanics.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static RimuruDev.Helpers.Tag;

public class BrokenFloor : MonoBehaviour
{
    private RatCharacterData player;
    private AudioManager audioManager;
    private Health playerHealth;

    void Start()
    {
        player = FindObjectOfType<RatCharacterData>();
        audioManager = FindObjectOfType<AudioManager>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            playerHealth.TakeHit(1);  
            StartCoroutine(PlayerStuck());
        }
    }

    IEnumerator PlayerStuck()
    {
        yield return new WaitForSeconds(0.15f);
        player.isStopped = true;
        yield return new WaitForSeconds(0.4f);
        player.isStopped = false;
    }
}
