using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenFloor : MonoBehaviour
{
    [SerializeField] private string AudioName;
    private Player player;
    private AudioManager audioManager;
    private Health playerHealth;

    void Start()
    {
        player = FindObjectOfType<Player>();
        audioManager = FindObjectOfType<AudioManager>();
        playerHealth = FindObjectOfType<Health>();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            playerHealth.TakeHit(1);  
            audioManager.PlayClip(AudioName);
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
