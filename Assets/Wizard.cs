using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public Element element;
    public bool selected;
    public GameController gameController;
    public GameObject bulletPrefab;
    float fireRate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Shoot(); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonUp(0)) 
        {
            selected = false;
        }

        if (gameController.gamePaused && selected && Input.GetMouseButton(0)) 
        {
            transform.position = cursorPos;
        }
    }

    private void OnMouseDown() 
    {
        selected = true;
    }

    private void Shoot()
    {
        if (gameController.gamePaused) {
            Invoke("Shoot", fireRate);
            return;
        }
        foreach (Enemy enemy in gameController.enemies)
        {
            if (enemy != null)
            {
                if (Vector2.Distance(enemy.transform.position, transform.position) <= 3)
                {
                    Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
                    bullet.gameController = gameController;
                    bullet.target = enemy.transform;
                    bullet.element = element;
                    Invoke("Shoot", fireRate);
                    return;
                }
            }
        }
        Invoke("Shoot", fireRate);
    }
}
