using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class TargetSelection : MonoBehaviour
{
    //variables
    private Target currentTarget;
    public UnityEvent<Target> onTargetChange;
    private List<Target> targets = new List<Target>();
    private List<string> ignoringTags = new List<string>();

    //getters
    public int TargetsCount => targets.Count;
    public Transform CurrentTarget => currentTarget.transform;

    //methods
    private void ChangeTarget()
    {
        if(targets.Count > 0)
        {
            Target.TargetComparator targetComparer = new Target.TargetComparator();
            targets.Sort(targetComparer);
            Target newTarget = targets[0];
        
            if(newTarget != currentTarget)
            {
                currentTarget = newTarget;
                onTargetChange.Invoke(currentTarget);
            }
        }
    }

    //unity methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Target newTarget) && !ignoringTags.Contains(collision.tag))
        {  
            targets.Add(newTarget);
            ChangeTarget();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Target exitTarget) && targets.Contains(exitTarget))
        {
            targets.Remove(exitTarget);
            if (currentTarget == exitTarget) ChangeTarget();
        }
    }

}