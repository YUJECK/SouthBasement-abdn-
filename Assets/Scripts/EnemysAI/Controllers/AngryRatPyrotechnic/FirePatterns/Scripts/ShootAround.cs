using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootAround", menuName = "ShootingPatterns/ShootAround")]
public class ShootAround : ShootingPattern
{
    [SerializeField] private GameObject[] projectiles;
    private Coroutine throwProjectile;
    public override void StartPattern(Shooting shooting)
    {
        isWork = true;
        onEnter.Invoke();
        throwProjectile = GameManager.instance.StartCoroutine(ThrowProjectile(shooting));
    }
    public override void StopPattern(Shooting shooting)
    {
        isWork = false;
        if (throwProjectile != null) PlayerController.instance.StopCoroutine(throwProjectile);
        onExit.Invoke();
    }

    private IEnumerator ThrowProjectile(Shooting shooting)
    {
        if(projectiles.Length !=0)
        {
            for (int i = 0; i < 8; i++)
            {
                yield return new WaitForSeconds(0.2f);
                shooting.Shoot(projectiles[Random.Range(0, projectiles.Length)], 35*i, 2000f);
            }
        }
        StopPattern(shooting);
    }
}
