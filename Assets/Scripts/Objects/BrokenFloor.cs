using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenFloor : MonoBehaviour
{
    private PlayerController player;
    private AudioManager audioManager;
    private PlayerHealth playerHealth;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        audioManager = FindObjectOfType<AudioManager>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            playerHealth.TakeHit(8);  
            StartCoroutine(PlayerStuck());
        }
    }

    IEnumerator PlayerStuck()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.isPlayerStopped = true;
        yield return new WaitForSeconds(0.4f);
        GameManager.isPlayerStopped = false;
    }
}
