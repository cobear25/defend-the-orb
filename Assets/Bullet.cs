using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public GameController gameController;
    float speed = 5f;
    public Element element;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.gamePaused) { return; }
        if (target != null)
        {
            Debug.Log(speed);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            // rb.MovePosition(transform.position + (target.position - transform.position) * Time.deltaTime * speed);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate() {
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().Hit(element);
            Destroy(gameObject);
        }
    }
}
