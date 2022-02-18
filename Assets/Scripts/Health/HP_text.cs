using UnityEngine;
using UnityEngine.UI;
public class HP_text : MonoBehaviour
{
    public Health Player_Health;
    public Text health_text;
    string Text;

    // Update is called once per frame
    void Update()
    {
        Text = Player_Health.health.ToString() + "/" + Player_Health.maxHealth.ToString();
        health_text.text = Text;
    }
}
