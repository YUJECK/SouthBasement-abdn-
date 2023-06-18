using System.Collections;
using UnityEngine;

namespace SouthBasement.Infrastucture
{
    public interface ICoroutineRunner
    {
        Coroutine Run(IEnumerator coroutine);
        void Stop(IEnumerator coroutine);
        void Stop(Coroutine coroutine);
    }
}