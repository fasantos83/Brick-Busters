using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScreen : MonoBehaviour {

    public string nextLevel;

    private void Update() {
        if (Input.anyKeyDown) {
            PlayerPrefs.SetInt("currentScore", GameManager.instance.GetCurrentScore());
            PlayerPrefs.SetInt("life", GameManager.instance.GetLife());

            StartCoroutine(ShowMessage());
            Destroy(GameManager.instance.gameObject);
            SceneManager.LoadScene(nextLevel);
        }
    }

    private IEnumerator ShowMessage() {
        yield return new WaitForSeconds(2f);
    }
}
