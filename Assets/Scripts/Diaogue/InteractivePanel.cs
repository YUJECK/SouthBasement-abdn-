using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractivePanel : MonoBehaviour
{
    [SerializeField]private Image VizualizatorSprite;
    [SerializeField]private Sprite YesSprite;
    [SerializeField]private Sprite NoSprite;
    [SerializeField]private Canvas Cloud;

    public bool isActive;
    public bool answer;
    [SerializeField]private UnityEvent yesAction;
    [SerializeField]private UnityEvent noAction;

    private void Update()
    {
        if(isActive)
        {
            if(Input.GetKeyDown(KeyCode.Q)) // Смена ответа
                ChangeAnswer();
            
            if(Input.GetKeyDown(KeyCode.R)) // Выбор ответа
            {
                if(answer)  yesAction.Invoke();
                if(!answer) noAction.Invoke();
            }
        }
    }
    

    public void ActivePanel() // Активировать панель
    {
        isActive = true;
        Cloud.enabled = true;
    }
    public void DisablePanel()
    {
        isActive = true;
        Cloud.enabled = true;
    }

    private void ChangeAnswer() 
    { 
        answer = !answer; 
        if(answer) VizualizatorSprite.sprite = YesSprite;
        else VizualizatorSprite.sprite = NoSprite;
    }  //Смена ответа
}