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
        if(ConsoleText != null)
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
            gameObject.SetActive(!gameObject.active);
    }
}
