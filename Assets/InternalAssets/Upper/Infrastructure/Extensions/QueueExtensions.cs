﻿using System.Collections.Generic;

namespace SouthBasement.Extensions.DataStructures
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