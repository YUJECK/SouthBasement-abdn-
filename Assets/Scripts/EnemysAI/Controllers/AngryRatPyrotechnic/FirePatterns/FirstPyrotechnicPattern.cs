using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class FirstPyrotechnicPattern : ShootingPattern 
{
    [SerializeField] private GameObject petard;
    public override void StartPattern(Shooting shooting)
    {
        onEnter.Invoke();
        isWork = true;
        PlayerController.instance.StartCoroutine(ThrowPetard(shooting));
    }
    public override void StopPattern(Shooting shooting)
    {
        isWork = false;
        Debug.Log("[Test]: Exit");
        onExit.Invoke();
    }

    private IEnumerator ThrowPetard(Shooting shooting)
    {
        yield return new WaitForSeconds(0.2f);
        shooting.Shoot(petard);
        yield return new WaitForSeconds(0.2f);
        StopPattern(shooting);
    }
}