using System.Collections;
using UnityEngine;
using EnemysAI;

[CreateAssetMenu(fileName = "ShootAround", menuName = "ShootingPatterns/ShootAround")]
public class ShootAround : ShootingPattern
{
    [SerializeField] private GameObject[] projectiles;
    private Coroutine throwProjectile;
    public override void StartPattern(Shooting shooting)
    {
        _isWork = true;
        onEnter.Invoke();
        throwProjectile = GameManager.instance.StartCoroutine(ThrowProjectile(shooting));
    }
    public override void StopPattern(Shooting shooting)
    {
        _isWork = false;
        if (throwProjectile != null) GameManager.instance.StopCoroutine(throwProjectile);
        onExit.Invoke();
    }

    private IEnumerator ThrowProjectile(Shooting shooting)
    {
        if (projectiles.Length != 0)
        {
            for (int i = 0; i < 8; i++)
            {
                yield return new WaitForSeconds(0.01f);
                shooting.Shoot(projectiles[Random.Range(0, projectiles.Length)], 40f * i, 10f);
            }
        }
        StopPattern(shooting);
    }
}
