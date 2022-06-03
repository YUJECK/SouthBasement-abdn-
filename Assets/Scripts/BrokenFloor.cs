using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenFloor : MonoBehaviour
{
    private Player player;
    private AudioManager audioManager;
    private Health playerHealth;

    void Start()
    {
        player = FindObjectOfType<Player>();
        audioManager = FindObjectOfType<AudioManager>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
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
