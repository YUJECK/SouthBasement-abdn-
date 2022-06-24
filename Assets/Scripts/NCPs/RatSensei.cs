using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSensei : MonoBehaviour
{
    private Player player;
    private PlayerAttack playerAttack;
    public int agility;
    public int strength;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }


    //Еффекты
    public void Agility() { player.dashDuration += player.dashDuration * 0.05f; agility++;}
    public void Strength() { playerAttack.damageBoost++; strength++;}
}