using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatCatchers : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Sprite CloseSprite;
    public bool isClose = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isClose)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<HealthEnemy>().TakeHit(12);
                sprite.sprite = CloseSprite;
                isClose = true;
            }
        }
    }
}