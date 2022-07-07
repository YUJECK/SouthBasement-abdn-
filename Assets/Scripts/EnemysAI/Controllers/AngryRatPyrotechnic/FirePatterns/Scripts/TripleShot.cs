using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TripleShot", menuName = "ShootingPatterns/TripleShot")]
public class TripleShot : ShootingPattern
{
    [SerializeField] private GameObject[] projectiles;
    private Coroutine tripleShot;
    public override void StartPattern(Shooting shooting)
    {
        isWork = true;
        onEnter.Invoke();
        tripleShot = GameManager.instance.StartCoroutine(ThrowProjectile(shooting));
    }
    public override void StopPattern(Shooting shooting)
    {
        isWork = false;
        if (tripleShot != null) GameManager.instance.StopCoroutine(tripleShot);
        onExit.Invoke();
    }

    private IEnumerator ThrowProjectile(Shooting shooting)
    {
        if (projectiles.Length != 0)
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.2f);
                shooting.Shoot(projectiles[Random.Range(0, projectiles.Length)], Random.Range(-30f, 30f), 20f);
            }
        }
        StopPattern(shooting);
    }
}
