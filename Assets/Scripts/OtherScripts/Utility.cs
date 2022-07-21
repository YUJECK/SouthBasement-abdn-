using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class Utility
{
    public static void InvokeMethod<T>(UnityAction<T> method, T argument, float delay) => GameManager.instance.StartCoroutine(_InvokeMethod(method, argument, delay));
    private static IEnumerator _InvokeMethod<T>(UnityAction<T> method, T argument, float delay)
    {
        yield return new WaitForSeconds(delay);
        method.Invoke(argument);
    }
}