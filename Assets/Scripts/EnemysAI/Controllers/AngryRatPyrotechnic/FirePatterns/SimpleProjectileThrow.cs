using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class SimpleProjectileThrow : ShootingPattern 
{
    [SerializeField] private GameObject projectile;
    private Coroutine throwProjectile;
    public override void StartPattern(Shooting shooting, float startTimeOffset)
    {

        isWork = true;
        onEnter.Invoke();
        throwProjectile = PlayerController.instance.StartCoroutine(ThrowProjectile(shooting));
    }
    public override void StopPattern(Shooting shooting)
    {
        isWork = false;
        if (throwProjectile != null) PlayerController.instance.StopCoroutine(throwProjectile);
        onExit.Invoke();
    }

    private IEnumerator ThrowProjectile(Shooting shooting)
    {
        yield return new WaitForSeconds(0.2f);
        shooting.Shoot(projectile, 0f, 550f);
        StopPattern(shooting);
    }
}