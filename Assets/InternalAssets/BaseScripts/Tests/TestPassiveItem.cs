using TheRat.InventorySystem;
using UnityEngine;

namespace TheRat.InternalAssets.BaseScripts.Tests
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
    }
}