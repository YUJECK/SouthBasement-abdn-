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
    public InputField ConsoleInput;
    public Text ConsoleText;
    public Text InfoText;
    public Commands[] commands;
    private bool showInfo = false;
    private bool stopConsole = false;
    private bool inputInConsole = false;
    private string newText;
    static int MessegesCount = 0;

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject box;
    private Player player;
    private RatAttack playerAttack;
    private InventoryManager inventory;

    public enum Mode{
        ConsoleMessege,
        Info
    }

    private void Start() 
    {
        ConsoleText = GetComponentInChildren<Text>(); 
        player = FindObjectOfType<Player>();
        inventory = FindObjectOfType<InventoryManager>();
        playerAttack = FindObjectOfType<RatAttack>();
    }
    private void ClearConsole()
    {
        ConsoleText.text = "";
        MessegesCount = 0;
    }
    public void DisplayText(string text, Mode mode)
    {
        if(ConsoleText != null && mode == Mode.ConsoleMessege)
        {
            if (MessegesCount > 18) 
                ClearConsole();
            ConsoleText.text += "\n" + "<Console> " + text;
            MessegesCount++;
        }
        else
        {
            InfoText.text = 
            "----UnityInfo----: " + "\n" + 
            "Time: " + Time.time + "\n" + 
            "Fps: " + GetFps() +
            "----PlayerInfo----: " + "\n" + 
            "Damage: " + playerAttack.damage + "\n" + 
            "Speed: " + player.speed + "\n" + 
            "Movement: " + player.movement + "\n" + 
            "Position: " + player.gameObject.transform.position + "\n" +
            "----InventoryInfo----: " + "\n"+
            "   --FoodSlot--" + "\n";
            if(inventory.foodItems[inventory.activeFoodSlot].food != null)
            {
                InfoText.text += "Name: " + inventory.foodItems[inventory.activeFoodSlot].food.name +"\n"+
                "Uses: " + inventory.foodItems[inventory.activeFoodSlot].objectOfItem.GetComponent<ItemInfo>().uses +"\n";
            }
        }
    }

    public int GetFps() { return (int)(1.0f / Time.deltaTime); } //Fps
    public void OnePunch() { playerAttack.damageBoost = 1000; } //Сделать игрока очень сильным
    public void SpawnEnemy() { Instantiate(enemy, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), Quaternion.identity); } //Спавн врага
    public void SpawnBox() { Instantiate(box, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), Quaternion.identity); } //Спавн коробки
    public void PathVisualization(bool active) { FindObjectOfType<Grid>().PathVisualization(active); } //Визуалищ=зация путя врагов
    public void ShowGrid(){FindObjectOfType<Grid>().ShowGrid();}

    private void Update()
    {
        //Включение/выключение консоли
        if(Input.GetKeyDown(KeyCode.LeftAlt))
            ConsoleText.gameObject.SetActive(!ConsoleText.gameObject.active);   
        

        if(!stopConsole)
        {
            //Fps
            if(Input.GetKeyDown(KeyCode.F1)) showInfo = !showInfo;
            if(showInfo) DisplayText(GetFps().ToString(), Mode.Info);
        }


        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            stopConsole = !stopConsole;
            inputInConsole = !inputInConsole;

            if(!inputInConsole)
            {
                newText = ConsoleInput.text;
                Time.timeScale = 1f;
                foreach(Commands comm in commands)
                    comm.CheckCommand(newText);
            } 
            else newText = "";
        }
        if(inputInConsole) 
        {
            ConsoleInput.gameObject.SetActive(true);
            ConsoleInput.ActivateInputField();
            Time.timeScale = 0.1f;
        }
        else ConsoleInput.gameObject.SetActive(false);
    }
}
