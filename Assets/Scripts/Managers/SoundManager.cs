using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    public AudioClip ballBounce;
    public AudioClip ballDead;
    public AudioClip brickBroken;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public void PlayBrickBrokenSound() {
        AudioSource.PlayClipAtPoint(brickBroken, transform.position);
    }

    public void PlayBallBounceSound() {
        AudioSource.PlayClipAtPoint(ballBounce, transform.position);
    }

    public void PlayBallDeadSound() {
        AudioSource.PlayClipAtPoint(ballDead, transform.position);
    }
}
