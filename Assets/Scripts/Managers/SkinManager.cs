using UnityEngine;

[System.Serializable] public class SkinPeace
{
    public SpriteRenderer spriteRenderer;
    public bool use = false;
    public Sprite defaultPeace;
    public Sprite sprintPeace;
    
    public SkinPeace(Sprite defaultPeace, Sprite sprintPeace)
    {
        this.defaultPeace = defaultPeace;
        this.sprintPeace = sprintPeace;
        use = true;
    }
    public void PutOn(Sprite clothes) { spriteRenderer.sprite = clothes; }
    //public void PutOff() { spriteRenderer.sprite = ; }
}
public class SkinManager : MonoBehaviour
{
    SkinPeace hat;
    SkinPeace face;
    SkinPeace body;
    SkinPeace legs;
    SkinPeace feet;

    public void PutHat()
    {
        hat.
    }
    public void PutHat()
    {

    }
    public void PutHat()
    {

    }
    public void PutHat()
    {

    }
    public void PutHat()
    {

    }
}
