using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private MoveControllerPreset preset;

    void Start()
    {
        preset.Init(this);
    }
}
