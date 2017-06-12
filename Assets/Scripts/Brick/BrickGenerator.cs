using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGenerator : MonoBehaviour {

    public static BrickGenerator instance;

	public GameObject brickPrefab;
    public GameObject brickSpawnZone;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void CreateBlockGroup(Brick[] bricks) {
		Bounds bounds = brickPrefab.GetComponent<SpriteRenderer>().bounds;
		float blockWidth = bounds.size.x;
		float blockHeight = bounds.size.y;
		float screenWidth, screenHeight, widthMultiplier;
		int columns;
		GetBlockInformation (blockWidth, out screenWidth, out screenHeight, out columns, out widthMultiplier);
		GameManager.instance.SetTotalBricks(bricks.Length * columns);
		for(int i = 0; i < bricks.Length; i++){
            Brick brick = bricks[i];
			for(int j = 0; j < columns; j++) {
				GameObject brickObect = Instantiate (brickPrefab);
                brickObect.GetComponent<BrickController>().SetScore(brick.brickValue);
                brickObect.GetComponent<SpriteRenderer>().color = brick.color;
                brickObect.transform.position = new Vector3 (-(screenWidth * 0.5f) + (j * blockWidth * widthMultiplier) + 0.8f, (screenHeight * 0.5f) - (i * blockHeight) + 1.25f, 0);
                brickObect.transform.localScale = new Vector3 (brickObect.transform.localScale.x * widthMultiplier, brickObect.transform.localScale.y, 1);
			}
		}
	}

	private void GetBlockInformation(float blockWidth, out float screenWidth, out float screenHeight, out int columns, out float widthMultiplier){
		Bounds bounds = brickSpawnZone.GetComponent<SpriteRenderer>().bounds;
        screenWidth = bounds.size.x;
        screenHeight = bounds.size.y;
		columns = (int)(screenWidth / blockWidth);
		widthMultiplier = screenWidth / (columns * blockWidth);
	}
}
