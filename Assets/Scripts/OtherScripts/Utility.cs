using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class Utility
{
    public enum CheckNumber
    {
        Much,
        Less
    }
    public static void InvokeMethod(UnityAction method, float delay) => GameManager.instance.StartCoroutine(_InvokeMethod(method, delay));
    public static void InvokeMethod<T>(UnityAction<T> method, T argument, float delay) => GameManager.instance.StartCoroutine(_InvokeMethod(method, argument, delay));
    private static IEnumerator _InvokeMethod(UnityAction method, float delay)
    {
        yield return new WaitForSeconds(delay);
        method.Invoke();
    }
    private static IEnumerator _InvokeMethod<T>(UnityAction<T> method, T argument, float delay)
    {
        yield return new WaitForSeconds(delay);
        method.Invoke(argument);
    }

    public static void ChechNumber(ref int ownNumber, int verificationNumber, int finalNumber, CheckNumber checkNumber)
    {
        if (checkNumber == CheckNumber.Much)
            if (ownNumber > verificationNumber) ownNumber = finalNumber;
        else if (checkNumber == CheckNumber.Less)
            if (ownNumber < verificationNumber) ownNumber = finalNumber;
    }
    public static Vector2 InvertVector2(Vector2 original) => original * new Vector2(-1f, -1f);
}