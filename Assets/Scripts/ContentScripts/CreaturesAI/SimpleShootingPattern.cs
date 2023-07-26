using System.Collections;
using UnityEngine;

[CreateAssetMenu()]
public class SimpleShootingPattern : ShootingPattern
{
    [SerializeField] private GameObject projectile;
    public override IEnumerator Pattern(Shooting shooting)
    {
        shooting.Shoot(projectile, 30f, 0f, 0f, ForceMode2D.Impulse);
        isFinished = true;
        
        yield return null;
    }
}
