using System.Collections;
using UnityEngine;

[CreateAssetMenu()]
public class RoundShootingPattern : ShootingPattern
{
    [SerializeField] private GameObject projectile;

    public override IEnumerator Pattern(Shooting shooting)
    {
        for (int i = 0; i < 9; i++)
        {
            shooting.Shoot(projectile, 30f, i * 40f, 0f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
        }

        isFinished = true;
    }
}