using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class TargetSelection : MonoBehaviour
{
    private Target currentTarget;
    public UnityEvent<Target> onTargetChange;
    private List<Target> targets = new List<Target>();
    private List<string> ignoringTags = new List<string>();

    //methods
    private void ChangeTarget()
    {
        Target newTarget = FindBestTarget();
        
        if(newTarget != currentTarget)
        {
            currentTarget = newTarget;
            onTargetChange.Invoke(currentTarget);
        }
    }
    private Target FindBestTarget()
    {
        if (targets.Count > 0)
        {
            Target bestTarget = targets[0];

            foreach (Target nextTarget in targets)
            {
                if (nextTarget.Priority > bestTarget.Priority)
                    bestTarget = nextTarget;
            }

            return bestTarget;
        }
        else return null;
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