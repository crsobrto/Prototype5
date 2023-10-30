using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro
using UnityEngine.SceneManagement; // Used for restarting the game
using UnityEngine.UI; // Used for interacting with buttons

public class GameManager : MonoBehaviour
{
    public GameObject titleScreen;
    public List<GameObject> targets; // Make a list that holds game objects
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    public bool isGameActive;
    private float spawnRate = 1.0f;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count); // targets.Count gets the number of elements in the targets list
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score; // Display the current score in the game
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true); // Activate the restart button
        gameOverText.gameObject.SetActive(true); // Activate the game over text
        isGameActive = false; // The game is now over
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the currently active scene for the player
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true; // The game is active when it is started
        score = 0;
        titleScreen.gameObject.SetActive(false); // Disable the title screen when the player starts the game

        StartCoroutine(SpawnTarget());
        UpdateScore(0); // Score = 0 at start of the game
    }
}
