using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Brick {
    public int brickValue;
    public Color color;

    public Brick(int brickValue, Color color) {
        this.brickValue = brickValue;
        this.color = color;
    }
}

public class GameManager : MonoBehaviour {

    public static GameManager instance;
   
    public BallController ballController;
    public Text lifeText;
    public Text scoreText;
    public Text hiScoreText;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject levelCompleteScreen;
    public Brick[] bricks;

    private bool gameActive = false;
    private bool gameOver = false;
    private bool levelComplete = false;
    private int life = 3;
    private int currentScore = 0;
    private int currentHiScore;
    private int totalBricks;
    private int brokenBricks;

    private void Awake() {
        Time.timeScale = 1f;
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        //Load score and lives from PlayerPrefs
        currentScore = PlayerPrefs.GetInt("currentScore");
        life = PlayerPrefs.GetInt("life");
        currentHiScore = PlayerPrefs.GetInt("hiScore");

        brokenBricks = 0;
        BrickGenerator.instance.CreateBlockGroup(bricks);
        UpdateLifeText();
        UpdateScore(0);
        LevelStart();
    }

    private void Update () {
        if (!gameActive && (Input.GetKeyDown(KeyCode.Space))) {
            gameActive = true;
            ballController.ActivateBall(true);
        }else if(!gameOver && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))){
            MenuManager.instance.PauseGame(pauseScreen);
        }
	}

    private void LevelStart() {
        gameActive = false;
        gameOver = false;
        levelComplete = false;
    }

    public void UpdateScore(int score) {
        currentScore += score;
        if(currentScore > currentHiScore) {
            currentHiScore = currentScore;
            PlayerPrefs.SetInt("hiScore", currentHiScore);
        }
        scoreText.text = "SCORE: " + currentScore;
        hiScoreText.text = "HI-SCORE: " + currentHiScore;
        CheckLevelComplete();
    }

    public void RespawnBall() {
        gameActive = false;
        life--;
        UpdateLifeText();
        CheckGameOver();
    }

    public void UpdateLifeText() {
        lifeText.text = "LIVES: " + life;
    }

    public void CheckLevelComplete() {
        if (brokenBricks == totalBricks) {
            Time.timeScale = 0f;
            levelComplete = true;
            ballController.ActivateBall(false);
            levelCompleteScreen.SetActive(true);
        }
    }

    public void CheckGameOver() {
        if (life <= 0) {
            Time.timeScale = 0f;
            gameOver = true;
            ballController.ActivateBall(false);
            gameOverScreen.SetActive(true);
        }
    }

    public bool CanMove() {
        return !gameOver && !levelComplete;
    }

    public int GetCurrentScore() {
        return currentScore;
    }

    public int GetLife() {
        return life;
    }

    public void SetTotalBricks(int totalBricks) {
        this.totalBricks = totalBricks;
    }

    public void UpdateBrokenBricks() {
        brokenBricks++;
    }
}
