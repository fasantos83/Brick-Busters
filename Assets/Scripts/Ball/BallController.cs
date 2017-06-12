using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float maxHorizontalSpeed = 8f;
    public float initialSpeed = 5f;
    public bool ballActive = false;
    public Transform startPoint;

    private Rigidbody2D rb2D;

    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (!ballActive) {
            rb2D.velocity = Vector2.zero;
            transform.position = startPoint.position;
        } 

        float horizontalSpeed = rb2D.velocity.x;
        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -maxHorizontalSpeed, maxHorizontalSpeed);
        horizontalSpeed = horizontalSpeed == 0 ? 0.5f : horizontalSpeed; 

        float verticalSpeed = rb2D.velocity.y;
        verticalSpeed = verticalSpeed == 0 ? 0.5f : verticalSpeed;
        
        rb2D.velocity = new Vector2(horizontalSpeed, rb2D.velocity.y);
    }

    public void ActivateBall(bool toggle) {
        if (!ballActive && toggle) {
            rb2D.velocity = new Vector2(initialSpeed/2, initialSpeed);
        } else if (ballActive && !toggle) {

        }
        ballActive = toggle;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        SoundManager.instance.PlayBallBounceSound();
        if (collision.gameObject.CompareTag("Brick")){
            collision.gameObject.GetComponent<BrickController>().DestroyBrick();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Respawn")) {
            ActivateBall(false);
            GameManager.instance.RespawnBall();
            SoundManager.instance.PlayBallDeadSound();
        }
    }
}
