using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [Header("Другое")]
    private bool invisibleCadrs = false;
    public Animator healthBar;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public AudioManager audioManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        onDie.AddListener(DefaultOnDie);
        onHealthChange.Invoke(health, maxHealth);
    }
    public override void Heal(int heal)
    {
        health += heal;

        if (health > maxHealth)
            health = maxHealth;
        onHealthChange.Invoke(health, maxHealth);
    }
    public override void SetHealth(int newMaxHealth, int newHealth)
    {
        maxHealth = newMaxHealth;
        health = newHealth;

        if (health > maxHealth)
            health = maxHealth;
        onHealthChange.Invoke(health, maxHealth);
    }
    public override void TakeAwayHealth(int takeAwayMaxHealth, int takeAwayHealth)
    {
        maxHealth -= takeAwayMaxHealth;
        health -= takeAwayHealth;

        if (health > maxHealth)
            health = maxHealth;

        onHealthChange.Invoke(health, maxHealth);
        if (health <= 0)
            SceneManager.LoadScene("RestartMenu");
    }
    public override void TakeHit(int damage, float stunDuration = 0)
    {
        if (!invisibleCadrs)
        {
            health -= damage;
            audioManager.PlayClip("RatHurt");
            StartCoroutine(TakeHitVizualization());

            invisibleCadrs = true;
            healthBar.SetBool("InvisibleCadrs", true);
            StartCoroutine(InvisibleCadrs());

            onHealthChange.Invoke(health, maxHealth);

            if (health <= 0)
                SceneManager.LoadScene("RestartMenu");
        }
    }
    public override void PlusNewHealth(int newMaxHealth, int newHealth)
    {
        maxHealth += newMaxHealth;
        health += newHealth;

        if (health > maxHealth)
            health = maxHealth;
        onHealthChange.Invoke(health, maxHealth);
    }
    public IEnumerator InvisibleCadrs()
    {
        yield return new WaitForSeconds(1f);
        invisibleCadrs = false;
        healthBar.SetBool("InvisibleCadrs", false);
    }
}