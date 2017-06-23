using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour {

	public Text scoreText;

	void Start () {
		scoreText.text = PlayerPrefs.GetInt ("currentScore") + " Points!";
	}

	void Update () {
		if (Input.anyKeyDown) {
			StartCoroutine(ShowMessage());
			Destroy(GameManager.instance.gameObject);
			Destroy(MenuManager.instance.gameObject);
			Destroy(SoundManager.instance.gameObject);
			SceneManager.LoadScene("MainMenu");
		}
	}

	private IEnumerator ShowMessage() {
		yield return new WaitForSecondsRealtime(2f);
	}
}
