using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public Base centralBase;
    public GameObject enemyPrefab;
    public GameObject redWizardPrefab;
    public GameObject greenWizardPrefab;
    public GameObject blueWizardPrefab;
    public GameObject resumeButton;
    public Image redButton;
    public Image greenButton;
    public Image blueButton;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI timeText;

    public bool gamePaused = true;
    public int secondsRemaining = 10;
    public List<Enemy> enemies;
    public int enemiesHit = 0;
    public int coins = 9;
    float enemyFrequency = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Enemy>();
        Invoke("SpawnEnemy", enemyFrequency);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            redButton.enabled = true;
            greenButton.enabled = true;
            blueButton.enabled = true;
        } 
    }

    void SpawnEnemy()
    {
        if (gamePaused) { return; }
        Enemy enemy = Instantiate(enemyPrefab).GetComponent<Enemy>(); 
        float angle = Random.Range(0.0f, Mathf.PI * 2f);
        enemy.transform.position = new Vector2(Mathf.Cos(angle) * 10, Mathf.Sin(angle) * 10);
        enemy.centralBase = centralBase;
        enemy.gameController = this;
        enemy.speed = Random.Range(1f, 4f);
        enemy.element = (Element)Random.Range(0, 3);
        enemies.Add(enemy);
        if (enemyFrequency >= 0.5f)
        {
            enemyFrequency -= 0.05f;
        }
        Invoke("SpawnEnemy", enemyFrequency);
    }

    public void AddWizard(int element) 
    {
        if (!gamePaused) { return; }
        if (coins < 3) { return; }
        GameObject prefab = redWizardPrefab;
        switch (element) {
            case (int)Element.red:
                prefab = redWizardPrefab;
                redButton.enabled = false;
                break;
            case (int)Element.green:
                prefab = greenWizardPrefab;
                greenButton.enabled = false;
                break;
            case (int)Element.blue:
                prefab = blueWizardPrefab;
                blueButton.enabled = false;
                break;
        }
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Wizard wizard = Instantiate(prefab, cursorPos, Quaternion.identity).GetComponent<Wizard>(); 
        wizard.selected = true;
        wizard.gameController = this;
        coins -= 3;
        coinText.text = $"${coins}";
    }

    public void ResumeGame()
    {
        gamePaused = false;
        SpawnEnemy();
        secondsRemaining = 10;
        Invoke("CountDown", 1);
        resumeButton.SetActive(false);
        timeText.text = $"{secondsRemaining}";
    }

    void CountDown()
    {
        secondsRemaining--;
        timeText.text = $"{secondsRemaining}";
        if (secondsRemaining == 0)
        {
            gamePaused = true;
            resumeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Resume";
            resumeButton.SetActive(true);
        } else {
            Invoke("CountDown", 1);
        }
    }

    public void EnemyHit()
    {
        enemiesHit++;
        coins++;
        pointsText.text = $"Score: {enemiesHit}";
        coinText.text = $"${coins}";
    }
}
