using System;
using SouthBasement.InventorySystem;
using UnityEngine;

namespace SouthBasement.BaseScripts.Tests
{
    [CreateAssetMenu]
    public sealed class TestPassiveItem : PassiveItem
    {
        public override void OnPutOn()
        {
            Debug.Log("Put on");
        }

        public override void OnRun()
        {
            Debug.Log("Run");
        }

        public override Type GetItemType()
        {
            return typeof(PassiveItem);
        }
    }
}