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
            if (commandName == comm)
                if (action != null) action.Invoke();
        }
    };
    
    public InputField ConsoleInput;
    public Text ConsoleText;
    public Text InfoText;
    public Commands[] commands;
    private bool showInfo = false;
    private bool stopConsole = false;
    private bool inputInConsole = false;
    [SerializeField] private string newText;
    static int MessegesCount = 0;

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject box;
    private PlayerController player;
    private PlayerAttack playerAttack;
    private GameManager gameManager;
    private InventoryManager inventory;

    public enum Mode{
        ConsoleMessege,
        Info
    }

    private void Start() 
    {
        ConsoleText = GetComponentInChildren<Text>(); 
        player = FindObjectOfType<PlayerController>();
        inventory = FindObjectOfType<InventoryManager>();
        gameManager = FindObjectOfType<GameManager>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }
    
    //Всякое для конслои
    private void ClearConsole()
    {
        ConsoleText.text = "";
        MessegesCount = 0;
    }
    public void DisplayText(string text, Color color, Mode mode, string autor = "<Console>")
    {
        if(ConsoleText != null && mode == Mode.ConsoleMessege)
        {
            if (MessegesCount > 18) 
                ClearConsole();
            ConsoleText.color = color;
            ConsoleText.text += "\n" + autor + " " + text;
            MessegesCount++;
        }
        else if(InfoText != null && mode == Mode.Info)
        {
            InfoText.text = 
            "----UnityInfo----: " + "\n" + 
            "Time: " + Time.time + "\n" + 
            "Fps: " + GetFps() + "\n" +
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
                "Uses: " + inventory.foodItems[inventory.activeFoodSlot].objectOfItem.GetComponent<ItemInfo>().GetUses() +"\n";
            }
            InfoText.text += "   --MelleWeapon--" + "\n";
            if(inventory.melleWeapons[inventory.melleRangeActiveSlot].melleWeapon != null)
            {
                InfoText.text += "Name: " + inventory.melleWeapons[inventory.melleRangeActiveSlot].melleWeapon.name +"\n"+
                "Damage: " + inventory.melleWeapons[inventory.melleRangeActiveSlot].melleWeapon.damage +"\n"+
                "AttackRate: " + inventory.melleWeapons[inventory.melleRangeActiveSlot].melleWeapon.attackRate +"\n"+
                "AttackRange: " + inventory.melleWeapons[inventory.melleRangeActiveSlot].melleWeapon.attackRange+"\n";
            }
        }
    }

    
    //Методы для комманд в коммандной строки
    public void CommandsList()
    {
        DisplayText("", Color.white, Mode.ConsoleMessege);
        DisplayText("CommandList: ", Color.white, Mode.ConsoleMessege);
        for(int i = 0; i < commands.Length; i++)
        {
            DisplayText(commands[i].commandName, Color.white, Mode.ConsoleMessege);
        }
    }
    public int GetFps() { return (int)(1.0f / Time.unscaledDeltaTime); } //Fps
    public void OnePunch() { playerAttack.damageBoost = 1000; DisplayText("Damage boost - " + playerAttack.damageBoost.ToString(), Color.green,  Mode.ConsoleMessege); }//Сделать игрока очень сильным
    public void SpawnEnemy() { Instantiate(enemy, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), Quaternion.identity); } //Спавн врага
    public void SpawnBox() { Instantiate(box, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), Quaternion.identity); } //Спавн коробки
    public void GetCheese(){FindObjectOfType<GameManager>().CheeseScore(100);}
    public void GetHealth(){FindObjectOfType<PlayerHealth>().SetHealth(100, 100);}
    public void Ghost() { FindObjectOfType<PlayerController>().GetComponent<Collider2D>().isTrigger = true; DisplayText("Ghost - true", Color.green, Mode.ConsoleMessege); }
    public void ResetGhost() { FindObjectOfType<PlayerController>().GetComponent<Collider2D>().isTrigger = false; DisplayText("Ghost - false", Color.green, Mode.ConsoleMessege); }
    public void Give(string name)
    {
        GameObject item = null;
        ItemClass classOFItem = ItemClass.Food;
        //Поиск предмета в разныъ листах
        for (int i = 0; i < gameManager.Food.Count; i++)
        {
            if (name == gameManager.Food[i].GetComponent<ItemInfo>().itemName)
            {
                classOFItem = ItemClass.Food;
                item = gameManager.Food[i];
            }
        }
        for (int i = 0; i < gameManager.MelleRange.Count; i++)
        {
            if (name == gameManager.MelleRange[i].GetComponent<ItemInfo>().itemName)
            {
                classOFItem = ItemClass.MelleRangeWeapon;
                item = gameManager.MelleRange[i];
            }
        }
        for (int i = 0; i < gameManager.ActiveItems.Count; i++)
        {
            if (name == gameManager.ActiveItems[i].GetComponent<ItemInfo>().itemName)
            {
                classOFItem = ItemClass.ActiveItem;
                item = gameManager.ActiveItems[i];
            }
        }
        for (int i = 0; i < gameManager.PassiveItems.Count; i++)
        {
            if (name == gameManager.PassiveItems[i].GetComponent<ItemInfo>().itemName)
            {
                classOFItem = ItemClass.PassiveItem;
                item = gameManager.PassiveItems[i];
            }
        }

        if (item == null) DisplayText("Предмет не найден", Color.red, Mode.ConsoleMessege);
        else
        {
            DisplayText("Предмет: " + item.GetComponent<ItemInfo>().itemName + " был выдан", Color.green, Mode.ConsoleMessege);
            if(classOFItem == ItemClass.Food) inventory.AddFood(item.GetComponent<FoodPickUp>().food, item);
            if(classOFItem == ItemClass.MelleRangeWeapon) inventory.AddMelleWeapon(item.GetComponent<MelleWeaponPickUp>().melleWeapon, item);
            if(classOFItem == ItemClass.ActiveItem) inventory.AddActiveItem(item.GetComponent<ActiveItemPickUp>().activeItem, item);
            //if(classOFItem == ItemClass.MelleRangeWeapon) inventory.AddFood(item.GetComponent<FoodPickUp>().food, item);
        }
    }
    
    private void Update()
    {
        //Включение/выключение консоли
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ConsoleText.gameObject.SetActive(!ConsoleText.gameObject.activeSelf);   
            InfoText.gameObject.SetActive(!InfoText.gameObject.activeSelf);
            showInfo = !showInfo;
        }
        if(!stopConsole && showInfo) DisplayText("", Color.white, Mode.Info);


        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            stopConsole = !stopConsole;
            inputInConsole = !inputInConsole;

            if (!inputInConsole)
            {
                newText = ConsoleInput.text;
                Time.timeScale = 1f;

                if (newText.Length != 0&& newText[0] == '/')
                {
                    foreach (Commands comm in commands)
                        comm.CheckCommand(newText);
                }
                else DisplayText(newText, Color.white, Mode.ConsoleMessege, "<Player>");
                
                //Написать смайлики
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
