using UnityEngine.UI;
using UnityEngine;

public class RatConsole : MonoBehaviour 
{
    public static Text ConsoleText;
    static int MessegesCount = 0;

    private void Start()
    {
        ConsoleText = GetComponent<Text>();
    }
    public static void DisplayText(string text)
    {
        if (MessegesCount > 18)
        {
            ConsoleText.text = "";
            MessegesCount = 0;
        }
        ConsoleText.text += "\n" + "<Console> " + text;
        MessegesCount++;
    }
}
