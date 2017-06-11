using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEffect : MonoBehaviour {

    public float lifetime = 1f;
    public Text scoreText;

    private void Update() {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) {
            Destroy(gameObject);
        }
    }

    public void SetScore(int score) {
        scoreText.text = score.ToString();
    }

}
