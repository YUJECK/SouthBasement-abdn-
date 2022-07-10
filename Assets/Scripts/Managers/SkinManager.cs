using UnityEngine;

[System.Serializable] public class SkinPeace
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Skin skin;
    private bool active = false;
    public void PutOn(Sprite clothes) { active = true; spriteRenderer.sprite = clothes; spriteRenderer.gameObject.SetActive(true); }
    public void PutOff() { active = false; spriteRenderer.gameObject.SetActive(false); }
    public bool CheckSkin(Sprite spriteToCheck)
    {
        if (spriteRenderer.sprite == spriteToCheck) return true;
        else return false;
    }
    public bool GetActive() { return active; }
}
[System.Serializable] public class Skin
{
    public Sprite defaultPeace;
    public Sprite sprintPeace;
    
    public Skin(Sprite defaultPeace, Sprite sprintPeace)
    {
        this.defaultPeace = defaultPeace;
        this.sprintPeace = sprintPeace;
    }
}
public class SkinManager : MonoBehaviour
{
    //Части скина
    public SkinPeace hat;
    public SkinPeace onFace;
    public SkinPeace onBody;
    public SkinPeace onLegs;
    public SkinPeace onFeet;

    //Другое
    private PlayerController playerController;

    public void PutHat(Skin newSkin) { hat.skin = newSkin; hat.PutOn(newSkin.defaultPeace); }
    public void PutOnFace(Skin newSkin) { onFace.skin = newSkin; hat.PutOn(newSkin.defaultPeace); }
    public void PutOnBody(Skin newSkin) { onBody.skin = newSkin; hat.PutOn(newSkin.defaultPeace); }
    public void PutOnLegs(Skin newSkin) { onLegs.skin = newSkin; hat.PutOn(newSkin.defaultPeace); }
    public void PutOnFeet(Skin newSkin) { onFeet.skin = newSkin; hat.PutOn(newSkin.defaultPeace); }

    private void Start() { playerController = FindObjectOfType<PlayerController>(); }
    private void Update()
    {
        if(playerController.isSprinting)
        {
            if (hat.GetActive() && !hat.CheckSkin(hat.skin.sprintPeace)) hat.PutOn(hat.skin.sprintPeace);
            if (onFace.GetActive() && !onFace.CheckSkin(onFace.skin.sprintPeace)) onFace.PutOn(onFace.skin.sprintPeace);
            if (onBody.GetActive() && !onBody.CheckSkin(onBody.skin.sprintPeace)) onBody.PutOn(onBody.skin.sprintPeace);
            if (onLegs.GetActive() && !onLegs.CheckSkin(onLegs.skin.sprintPeace)) onLegs.PutOn(onLegs.skin.sprintPeace);
            if (onFeet.GetActive() && !onFeet.CheckSkin(onFeet.skin.sprintPeace)) onFeet.PutOn(onFeet.skin.sprintPeace);
        }
        else
        {
            if (hat.GetActive() && !hat.CheckSkin(hat.skin.defaultPeace)) hat.PutOn(hat.skin.defaultPeace);
            if (onFace.GetActive() && !onFace.CheckSkin(onFace.skin.defaultPeace)) onFace.PutOn(onFace.skin.defaultPeace);
            if (onBody.GetActive() && !onBody.CheckSkin(onBody.skin.defaultPeace)) onBody.PutOn(onBody.skin.defaultPeace);
            if (onLegs.GetActive() && !onLegs.CheckSkin(onLegs.skin.defaultPeace)) onLegs.PutOn(onLegs.skin.defaultPeace);
            if (onFeet.GetActive() && !onFeet.CheckSkin(onFeet.skin.defaultPeace)) onFeet.PutOn(onFeet.skin.defaultPeace);
        }
    }
}