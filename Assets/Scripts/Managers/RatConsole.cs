using UnityEngine.UI;
using UnityEngine;

public class RatConsole : MonoBehaviour 
{
    public static Text ConsoleText;
    public string[] commands;
    private bool showFps = false;
    private bool stopConsole = false;
    private bool inputInConsole = false;
    private string newText;
    static int MessegesCount = 0;

    private void Start() { ConsoleText = GetComponentInChildren<Text>(); }
    private static void ClearConsole()
    {
        ConsoleText.text = "";
        MessegesCount = 0;
    }
    public static void DisplayText(string text)
    {
        if(ConsoleText != null)
        {
            if (MessegesCount > 18) 
                ClearConsole();
            ConsoleText.text += "\n" + "<Console> " + text;
            MessegesCount++;
        }
    }


    public int GetFps()
    {
        return (int)(1.0f / Time.deltaTime);
    }

    private void Update()
    {
        //Включение/выключение консоли
        if(Input.GetKeyDown(KeyCode.LeftAlt))
            ConsoleText.gameObject.SetActive(!ConsoleText.gameObject.active);   
        

        if(!stopConsole)
        {
            //Fps
            if(Input.GetKeyDown(KeyCode.F1)) showFps = !showFps;
            if(showFps) DisplayText(GetFps().ToString() + " fps");
        }

        //Написать еще всякого
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            stopConsole = true;
            inputInConsole = true;
        }
        // if(inputInConsole) 
        //     newText = Input.GetKey()

        //Изменение скорости
    }
}
