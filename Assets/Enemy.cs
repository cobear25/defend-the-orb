using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element {
    red, green, blue
}

public class Enemy : MonoBehaviour
{
    public GameController gameController;
    public SpriteRenderer spriteRenderer;
    public Sprite redSprite;
    public Sprite greenSprite;
    public Sprite blueSprite;
    public Base centralBase;
    public Element element;
    Rigidbody2D rb;
    public float speed;
    int life = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        switch (element)
        {
            case Element.red:
                spriteRenderer.sprite = redSprite;
                break;
            case Element.green:
                spriteRenderer.sprite = greenSprite;
                break;
            case Element.blue:
                spriteRenderer.sprite = blueSprite;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // set the angle to face the base
        transform.right = -transform.position - centralBase.transform.position;
        if (gameController.gamePaused) { return; }
        transform.position = Vector2.MoveTowards(transform.position, centralBase.transform.position, speed * Time.deltaTime);
    }

    private void FixedUpdate() {
        // rb.MovePosition(transform.position + (centralBase.transform.position - transform.position) * Time.deltaTime * speed);
    }

    public void Hit(Element el)
    {
        Debug.Log($"hit by: {el}");
        switch (el)
        {
            case Element.red:
                if (this.element == Element.red)
                {
                    life -= 2;
                }
                else if (this.element == Element.green)
                {
                    life -= 3;
                }
                else 
                {
                    life -= 1;
                }
                break;
            case Element.green:
                if (this.element == Element.red)
                {
                    life -= 1;
                }
                else if (this.element == Element.green)
                {
                    life -= 2;
                }
                else 
                {
                    life -= 3;
                }
                break;
            case Element.blue:
                if (this.element == Element.red)
                {
                    life -= 3;
                }
                else if (this.element == Element.green)
                {
                    life -= 1;
                }
                else 
                {
                    life -= 2;
                }
                break;
        }
        if (life <= 0)
        {
            gameController.EnemyHit();
            Destroy(gameObject);
        }
    }
}
