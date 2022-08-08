using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    static public int damage;
    static public int health;
    static public int luck = 10;

    static public int GenerateChance() => Random.Range(0, 101) - luck;
}
