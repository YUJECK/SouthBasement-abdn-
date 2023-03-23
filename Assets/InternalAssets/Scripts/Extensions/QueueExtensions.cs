using System.Collections.Generic;

namespace Assets.InternalAssets.Scripts.Extensions
{
    public static class QueueExtensions
    {
        public static void EnqueueRange<TQueue>(this Queue<TQueue> queue, TQueue[] elements)
        {
            foreach (var element in elements)
                queue.Enqueue(element);
        }
    }
}