using UnityEngine;

public sealed class Target : MonoBehaviour
{
    [SerializeField] private int priority = 1;
    public int Priority => priority;
}
