using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour {

    public int brickValue;
    public GameObject scoreEffect;

    public void SetScore(int score) {
        brickValue = score;
    }

    public void DestroyBrick() {
        GameManager.instance.UpdateBrokenBricks();
        GameManager.instance.UpdateScore(brickValue);
        GameObject scoreObject = Instantiate(scoreEffect, transform.position, transform.rotation) as GameObject;
        scoreObject.GetComponent<ScoreEffect>().SetScore(brickValue);
        Destroy(gameObject);
    }
}
