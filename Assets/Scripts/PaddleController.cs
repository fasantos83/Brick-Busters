using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {

    public float speed;
    public float adjustSpeed;
    public float leftLimit = -7.6f;
    public float rightLimit = 7.6f;

    private float direction;

    private void FixedUpdate() {
        if (GameManager.instance.CanMove()) {
            direction = Input.GetAxis("Horizontal");
            float clampYPosition = transform.position.x + (direction * speed * Time.fixedDeltaTime);
            clampYPosition = Mathf.Clamp(clampYPosition, leftLimit, rightLimit);
            Vector3 newPosition = new Vector3(clampYPosition, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        other.rigidbody.velocity = new Vector2(other.rigidbody.velocity.x + (direction * adjustSpeed), other.rigidbody.velocity.y);
    }
}
