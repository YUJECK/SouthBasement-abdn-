using UnityEngine;

public class Weapons : MonoBehaviour
{
    public FireballSpawn fireball;
    public static Weapons instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
