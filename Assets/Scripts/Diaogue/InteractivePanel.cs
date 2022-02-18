using UnityEngine;
using UnityEngine.UI;

public class InteractivePanel : MonoBehaviour
{
    public bool isActive;
    public bool YesActive = false;
    public bool NoActive = true;
    public Image VizualizatorSprite;
    public Sprite YesSprite;
    public Sprite NoSprite;
    public Canvas Cloud;

    private void Update()
    {
        if (isActive)
        {
            //����� ������
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (YesActive)
                    NoActivation();
                else if (NoActive)
                    YesActivation();
            }
            //�����
            if (Input.GetKeyDown(KeyCode.LeftAlt))
                DisablePanel();
        }
        else
            Cloud.enabled = false;
    }

    public void NoActivation()
    {
        NoActive = true;
        YesActive = false;
        VizualizatorSprite.sprite = NoSprite;
    }
    public void YesActivation()
    {
        YesActive = true;
        NoActive = false;
        VizualizatorSprite.sprite = YesSprite;
    }
    public void ActivePanel()
    {
        isActive = true;
        GameManager.isActiveAnyPanel = true;
    }
    public void DisablePanel()
    {
        isActive = false;
        Cloud.enabled = false;
        GameManager.isActiveAnyPanel = false;
    }
}
