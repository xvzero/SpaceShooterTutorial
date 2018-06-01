using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float startWait, spawnWait, waveWait;

    Text scoreText, gameOverText, restartText;
    public static int score;
    public static bool gameOver;

    private bool restart;

    void Awake()
    {
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
        gameOverText = GameObject.FindWithTag("GameOverText").GetComponent<Text>();
        restartText = GameObject.FindWithTag("RestartText").GetComponent<Text>();
    }

    void Start()
    {
        gameOverText.text = "";
        restartText.text = "";
        score = 0;
        restart = false;
        gameOver = false;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (!gameOver)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    void Update()
    {
        if (gameOver)
        {
            gameOverText.text = "Game Over!";
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }

        scoreText.text = "Score: " + score;
    }
}
