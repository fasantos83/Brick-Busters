using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;

    public string firstLevel;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public void ResetTimeScale() {
        Time.timeScale = 1f;
    }

    public void NewGame() {
        SceneManager.LoadScene(firstLevel);

        PlayerPrefs.SetInt("currentScore", 0);
        PlayerPrefs.SetInt("life", 3);
        PlayerPrefs.SetInt("hiScore", 0);
    }

    public void PauseGame(GameObject pauseScreen) {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
        Time.timeScale = pauseScreen.activeSelf ? 0f : 1f;
    }

    public void RestartLevel() {
        PlayerPrefs.SetInt("currentScore", 0);
        PlayerPrefs.SetInt("life", 3);
        Destroy(GameManager.instance.gameObject);
        Destroy(BrickGenerator.instance.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu() {
        Destroy(GameManager.instance.gameObject);
        Destroy(BrickGenerator.instance.gameObject);
        ResetTimeScale();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        ResetTimeScale();
        Application.Quit();
    }

}
