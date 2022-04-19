using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class RatConsole : MonoBehaviour 
{
    [System.Serializable] public struct Commands
    {
        public string commandName;
        public UnityEvent action;

        public void CheckCommand(string comm)
        {
            if(commandName == comm)
                action.Invoke();
        }
    };

    public static Text ConsoleText;
    public InputField ConsoleInput;
    public Commands[] commands;
    private bool showFps = false;
    private bool stopConsole = false;
    private bool inputInConsole = false;
    private string newText;
    static int MessegesCount = 0;

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject box;
    private Player player;
    private RatAttack playerAttack;

    private void Start() 
    {
        ConsoleText = GetComponentInChildren<Text>(); 
        player = FindObjectOfType<Player>();
        playerAttack = FindObjectOfType<RatAttack>();
    }
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

    public int GetFps() { return (int)(1.0f / Time.deltaTime); } //Fps
    public void OnePunch() { playerAttack.damage = 1000; } //Сделать игрока очень сильным
    public void SpawnEnemy() { Instantiate(enemy, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), Quaternion.identity); } //Спавн врага
    public void SpawnBox() { Instantiate(box, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), Quaternion.identity); } //Спавн коробки


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
            stopConsole = !stopConsole;
            inputInConsole = !inputInConsole;

            if(!inputInConsole)
            {
                newText = ConsoleInput.text;
                foreach(Commands comm in commands)
                    comm.CheckCommand(newText);
            } 
            else newText = "";
        }
        if(inputInConsole) 
        {
            ConsoleInput.gameObject.SetActive(true);
            ConsoleInput.ActivateInputField();
        }
        else ConsoleInput.gameObject.SetActive(false);
    }
}
