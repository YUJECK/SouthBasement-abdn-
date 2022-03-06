using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private bool isOnTrigger;
    public string[] LevelNames;
    public bool isDrowf;
    public TextMeshProUGUI DialogCloudtext;
    
    [TextArea(3,3)]
    public string FirstBasementText;
    
    [TextArea(3,3)]
    public string SecondBasementText;
    
    [TextArea(3,3)]
    public string ThirdBasementText;
    public Text levelCounterText;
    public int LevelCounterInt = 1;
    private int _levelCounter = 0;

    void Awake()
    {
        LevelCounterInt = FindObjectOfType<GameManager>().LevelCounter;
        _levelCounter = FindObjectOfType<GameManager>().LevelCounter-1;

        if(isDrowf)
        {
            if(LevelCounterInt == 1)
                DialogCloudtext.SetText(FirstBasementText);
            
            if(LevelCounterInt == 2)
                DialogCloudtext.SetText(SecondBasementText);
            
            if(LevelCounterInt == 3)
                DialogCloudtext.SetText(ThirdBasementText);
        }
    }
    void Start()
    {
        levelCounterText = GameObject.FindGameObjectWithTag("LevelCounter").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            isOnTrigger = true;
    } 
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            isOnTrigger = false;
    } 

    void Update()
    {
        if(isOnTrigger)
        {
            if(Input.GetKeyDown(KeyCode.E))
                NextLevel();
        }
    }


    private void NextLevel()
    {
        _levelCounter++;
        SceneManager.LoadScene(LevelNames[_levelCounter]);
        LevelCounterInt++;
        FindObjectOfType<GameManager>().LevelCounter = LevelCounterInt;
        levelCounterText.text = "1 - " + LevelCounterInt.ToString();
    }
}
