using System.Collections;
using UnityEngine;
using EnemysAI;

[CreateAssetMenu(fileName = "ProjectileThrow", menuName = "ShootingPatterns/ProjectileThrow")]
public class SimpleProjectileThrow : ShootingPattern
{
    [SerializeField] private GameObject projectile;
    private Coroutine projectileThrow;
    public override void StartPattern(Shooting shooting)
    {
        _isWork = true;
        onEnter.Invoke();
        projectileThrow = GameManager.instance.StartCoroutine(ThrowProjectile(shooting));
    }
    public override void StopPattern(Shooting shooting)
    {
        _isWork = false;
        if (projectileThrow != null) GameManager.instance.StopCoroutine(projectileThrow);
        onExit.Invoke();
    }

    private IEnumerator ThrowProjectile(Shooting shooting)
    {
        yield return new WaitForSeconds(0.2f);
        shooting.Shoot(projectile, 0f, 20f);
        StopPattern(shooting);
    }
}