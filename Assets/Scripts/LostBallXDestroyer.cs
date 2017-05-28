using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to destroy the X when the player doesn't bounce the ball
public class LostBallXDestroyer : MonoBehaviour {

    void Start() {
        Destroy(gameObject, gameObject.GetComponent<Animation>()["LostBallX"].time);

    }

}
