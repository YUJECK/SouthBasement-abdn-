using UnityEngine;
using UnityEngine.Events;

public class Functions : MonoBehaviour //Скрипт для удаленного доступа к методам, например для анимаций
{
    [SerializeField] private UnityEvent methodName1;
    [SerializeField] public UnityEvent methodName2;
    [SerializeField] public UnityEvent methodName3;

    public void Method1(){methodName1.Invoke();}
    public void Method2(){methodName1.Invoke();}
    public void Method3(){methodName1.Invoke();}
}
