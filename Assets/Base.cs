using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Base : MonoBehaviour
{
    public GameController gameController;
    public int hitPoints = 100;
    public TextMeshPro hitPointsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            gameController.enemies.Remove(enemy);
            Destroy(other.gameObject, 0.1f);
            hitPoints--;
            hitPointsText.SetText($"{hitPoints}");
        }
    }
}
